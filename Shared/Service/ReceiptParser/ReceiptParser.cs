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
        public string ReceiptText { get; set; } = string.Empty;
        public Receipt ExtractedReceipt { get; set; } = new();

        public Receipt? ParseReceiptFromImageText(string ReceiptText)
        {
            var receipt = new Receipt();
            if (string.IsNullOrEmpty(ReceiptText))
            {
                return null;
            }
            else
            {
                ExtractReceiptLines();
                var ItemsExtracted = ExtractReceiptItems();
                if (ItemsExtracted)
                {
                    ExtractReceiptStore();
                    ExtractPurchaseDate();
                }
            }
            return null;
        }

        protected void ExtractReceiptLines()
        {
            if (!string.IsNullOrEmpty(ReceiptText) && !string.IsNullOrEmpty(ParseModel?.LinePattern))
            {
                ReceiptText = ReceiptText.Replace("\\n", "\n"); // Normalize the text

                var regex = new Regex(ParseModel.LinePattern);
                ReceiptLines = regex.Split(ReceiptText)
                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
                    .Select(line => line.Trim())
                    .ToList();
            }
        }

        protected bool ExtractReceiptItems()
        {
            bool itemsExtracted = false;
            var items = new List<Item>();
            string pattern = $@"{ParseModel.ItemPattern}";
            foreach (var line in ReceiptLines)
            {
                var match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    var item = new Item
                    {
                        Name = match.Groups["Name"].Value
                    };
                    items.Add(item);
                    itemsExtracted = true;
                }
                ExtractedReceipt.Items = items;
            }
            return itemsExtracted;
        }

        protected void ExtractReceiptStore()
        {
            string pattern = $@"{ParseModel.StorePattern}";
            foreach (var line in ReceiptLines)
            {
                var match = Regex.Match(line, pattern);

                if (match.Success)
                {
                    ExtractedReceipt.StoreName = match.Groups["StoreName"].Value;
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
                        var regex = new Regex(pattern);
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

    }
}
