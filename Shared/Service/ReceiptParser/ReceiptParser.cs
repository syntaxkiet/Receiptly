using Shared.Interface;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
    public class ReceiptParser : IReceiptStringParser
    {
        public ReceiptParseModel ParseModel { get; set; } = new();
        public List<string> ReceiptLines { get; set; } = new();
        public List<string> ItemLines { get; set; } = new();
        public string ReceiptText { get; set; } = string.Empty;
        public Receipt ExtractedReceipt { get; set; } = new();
        TimeSpan RegexTimeout { get; set; } = TimeSpan.FromSeconds(5);

        public Receipt? ParseReceiptFromImageText(string receiptText, ReceiptParseModel model)
        {
            ParseModel = model;
            ReceiptText = $@"{receiptText}";
            if (string.IsNullOrEmpty(ReceiptText))
            {
                return null;
            }
            else
            {
                ExtractReceiptLines();
                ExtractReceiptItemSingleLines();
                RemoveNonItemLines();
                CombineAndInsertKeyBeforeValue();
                CombineDualLines();
                ExtractReceiptItems();
                if (ItemLines.Count > 0)
                {
                    ExtractReceiptStore();
                    ExtractPurchaseDate();
                    return ExtractedReceipt;
                }
                else
                    return null;
            }
        }

        //protected void ExtractReceiptItems()
        //{

        //}
        protected void ExtractReceiptLines()
        {
            if (!string.IsNullOrEmpty(ReceiptText) && !string.IsNullOrEmpty(ParseModel?.LineSeparatorPattern))
            {
                ReceiptText = ReceiptText.Replace("\\n", "\n"); // Normalize the text

                var regex = new Regex(ParseModel.LineSeparatorPattern, RegexOptions.None, RegexTimeout);
                ReceiptLines = regex.Split(ReceiptText)
                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
                    .Select(line => line.Trim())
                    .ToList();
            }
        }

        protected void ExtractReceiptItemSingleLines()
        {
            string pattern = $@"{ParseModel.ItemLinePattern}";
            foreach (var line in ReceiptLines)
            {
                var regex = new Regex(pattern, RegexOptions.None, RegexTimeout);
                var match = regex.Match(line);
                if (match.Success)
                {
                    ItemLines.Add(line);
                }
            }
        }

        //Todo Finish and write test that asserts line above ecpression is included correctly in ItemLines
        protected void ExtractReceiptItemLineAbove()
        {
            var patterns = ParseModel.ExtractReceiptItemLineAboveExpressions;
            if (ReceiptLines == null || ReceiptLines.Count == 0 || patterns == null || patterns.Count == 0)
                return;

            var combinedLines = new List<string>();
            for (int i = 0; i < ReceiptLines.Count; i++)
            {
                var line = ReceiptLines[i];
                if (i + 1 < ReceiptLines.Count)
                {
                    foreach (var pattern in patterns)
                    {
                        var regex = new Regex(pattern.Replace("//", "/"), RegexOptions.None, RegexTimeout);
                        if (regex.IsMatch(ReceiptLines[i + 1]))
                        {
                            ItemLines.Add(line);
                        }
                    }
                }
            }
        }

        protected void RemoveNonItemLines()
        {
            var patterns = ParseModel.ExcludeLineFromItems;
            List<string> removeIndexes = new List<string>();
            foreach (var pattern in patterns)
            {
                for (var i = 0; i < ItemLines.Count - 1; i++)
                {
                    var line = ItemLines[i];
                    var regex = new Regex(pattern, RegexOptions.None, RegexTimeout);
                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        removeIndexes.Add(line);
                    }
                }
            }
            foreach (var line in removeIndexes)
            {
                ItemLines.Remove(line);
            }
        }

        protected void ExtractReceiptItems()
        {
            //ToDO add logic to separate price, ammount and total
            ExtractReceiptLines();
            ExtractReceiptItemSingleLines();
            RemoveNonItemLines();
            CombineAndInsertKeyBeforeValue();
            CombineDualLines();
            foreach (string line in ItemLines)
            {
                ExtractedReceipt.Items.Add(new Item() { Name = line });
            }
        }

        /// <summary>
        /// Matches on first occurence of ParseModel.StorePattern, saves whole line as Receipt.StoreName and breaks
        /// </summary>
        protected void ExtractReceiptStore()
        {
            string pattern = $@"{ParseModel.StorePattern}";
            foreach (var line in ReceiptLines)
            {
                var regex = new Regex(pattern, RegexOptions.None, RegexTimeout);
                var match = regex.Match(line);
                if (match.Success)
                {
                    ExtractedReceipt.StoreName = line;
                    break;
                }
            }
        }

        protected void ExtractPurchaseDate()
        {
            string pattern = $@"{ParseModel.PuchaseDatePattern}";
            foreach (var line in ReceiptLines)
            {
                var regex = new Regex(pattern, RegexOptions.None, RegexTimeout);
                var match = regex.Match(line);

                if (match.Success)
                {
                    DateTime extractedPurchaseDate;
                    if (DateTime.TryParse(match.Groups["PurchaseDate"].Value, out extractedPurchaseDate))
                    {
                        ExtractedReceipt.PurchaseDate = extractedPurchaseDate;
                    }
                }
            }
        }

        protected void CombineDualLines()
        {
            var patterns = ParseModel.DualLinePatterns;
            if (ItemLines == null || ItemLines.Count == 0 || patterns == null || patterns.Count == 0)
                return;

            var combinedLines = new List<string>();
            for (int i = 0; i < ItemLines.Count; i++)
            {
                var line = ItemLines[i];
                if (i + 1 < ItemLines.Count)
                {
                    foreach (var pattern in patterns)
                    {
                        var regex = new Regex(pattern.Replace("//", "/"), RegexOptions.None, RegexTimeout);
                        if (regex.IsMatch(ItemLines[i + 1]))
                        {

                            line = $"{line} {ItemLines[i + 1]}".Trim();
                            i++;
                        }
                    }
                }
                combinedLines.Add(line);
            }
            ItemLines = combinedLines;
        }
        //
        /// <summary>
        /// Runs after ExtractReceiptLines, ExtractReceiptItemLines, RemoveNonItemLines
        /// Runs all KVp in TriggersAtNextIndexInsertsBeforeOnCurrentIndex, extracts index matching key and inserts the line at previous line before matching expression in value
        /// </summary>
        public void CombineAndInsertKeyBeforeValue()
        {
            if (ItemLines == null || ItemLines.Count == 0 || ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex == null || ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex.Count == 0)
                return;
            var updatedLines = new List<string>();
            for (int i = 0; i < ItemLines.Count; i++)
            {
                var line = ItemLines[i];

                foreach (var rule in ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex)
                {
                    var triggerRegex = new Regex(rule.Key, RegexOptions.None, RegexTimeout);
                    var insertRegex = new Regex(rule.Value, RegexOptions.None, RegexTimeout);

                    if (i + 1 < ItemLines.Count && triggerRegex.IsMatch(ItemLines[i + 1]))
                    {
                        var matchedPart = insertRegex.Match(line).Value;
                        line = insertRegex.Replace(line, "").Trim();
                        line = $"{line} {ItemLines[i + 1]} {matchedPart}".Trim();
                        i++;
                    }
                }

                updatedLines.Add(line);
            }
            ItemLines = updatedLines;
        }
    }
}
