using Shared.Interface;
using Shared.Models;
using System;
using System.Collections.Generic;
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
                ExtractReceiptItemLines();
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

        protected void ExtractReceiptLines()
        {
            if (!string.IsNullOrEmpty(ReceiptText) && !string.IsNullOrEmpty(ParseModel?.LineSeparatorPattern))
            {
                ReceiptText = ReceiptText.Replace("\\n", "\n"); // Normalize the text

                var regex = new Regex(ParseModel.LineSeparatorPattern);
                ReceiptLines = regex.Split(ReceiptText)
                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
                    .Select(line => line.Trim())
                    .ToList();
            }
        }

        protected void ExtractReceiptItemLines()
        {
            string pattern = $@"{ParseModel.ItemLinePattern}";
            foreach (var line in ReceiptLines)
            {
                var match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    ItemLines.Add(line);
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
                    var match = Regex.Match(line, pattern);
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
            RemoveNonItemLines();
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
                var match = Regex.Match(line, pattern);

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
                var match = Regex.Match(line, pattern);

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
            if (ReceiptLines == null || ReceiptLines.Count == 0 || patterns == null || patterns.Count == 0)
                return;

            var combinedLines = new List<string>();
            for (int i = 0; i < ReceiptLines.Count; i++)
            {
                var line = ReceiptLines[i];
                bool isCombined = false;
                if (i + 1 < ReceiptLines.Count)
                {
                    foreach (var pattern in patterns)
                    {
                        var regex = new Regex(pattern.Replace("//", "/"));
                        if (regex.IsMatch(ReceiptLines[i + 1]))
                        {

                            line = $"{line} {ReceiptLines[i + 1]}".Trim();
                            i++;
                            isCombined = true;
                            break;
                        }
                    }
                }
                combinedLines.Add(line);
            }
            ReceiptLines = combinedLines;
        }

        public void CombineAndInsertKeyBeforeValue()
        {
            if (ReceiptLines == null || ReceiptLines.Count == 0 || ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex == null || ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex.Count == 0)
                return;
            var updatedLines = new List<string>();
            for (int i = 0; i < ReceiptLines.Count; i++)
            {
                var line = ReceiptLines[i];

                foreach (var rule in ParseModel.TriggersAtNextIndexInsertsBeforeOnCurrentIndex)
                {
                    var triggerRegex = new Regex(rule.Key);
                    var insertRegex = new Regex(rule.Value);

                    if (i + 1 < ReceiptLines.Count && triggerRegex.IsMatch(ReceiptLines[i + 1]))
                    {
                        var matchedPart = insertRegex.Match(line).Value;
                        line = insertRegex.Replace(line, "").Trim();
                        line = $"{line} {ReceiptLines[i + 1]} {matchedPart}".Trim();
                        i++;
                        break;
                    }
                }

                updatedLines.Add(line);
            }
            ReceiptLines = updatedLines;
        }

    }
}
