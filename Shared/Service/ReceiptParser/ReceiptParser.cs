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
        public Receipt? ParseReceiptFromImageText(string imageText)
        {
            var receipt = new Receipt();
            if (string.IsNullOrEmpty(imageText))
            {
                return null;
            }
            else
            {
                var items = ExtractReceiptItems(imageText);
                if (items != null)
                {
                    receipt.Items = items;
                    var storeName = ExtractReceiptStore(imageText);
                    receipt.StoreName = storeName ?? "Unable to extract store name";
                    var purchaseDate = ExtractPurchaseDate(imageText);
                    receipt.PurchaseDate = purchaseDate ?? DateTime.MinValue;
                }
            }
            return null;
        }
        protected List<Item>? ExtractReceiptItems(string receiptText)
        {
            var items = new List<Item>();
            string pattern = @"(?<Name>[A-ZÅÄÖa-zåäö\s]+?)\s(?<Quantity>\d*[,\.]?\d+)?\s?[A-Za-z]*\s?(?<UnitPrice>\d+[,\.]?\d+)?\s?(?<TotalPrice>\d+[,\.]?\d+)";
            var matches = Regex.Matches(receiptText, pattern);

            foreach (Match match in matches)
            {
                var item = new Item
                {
                    Name = match.Groups["Name"].Value.Trim(),
                };
                items.Add(item);
            }
            if (items.Count > 0)
            { return items; }

            else { return null; }
        }

        protected string? ExtractReceiptStore(string receiptText)
        { return null; }

        protected DateTime? ExtractPurchaseDate(string receiptText)
        { return null; }


    }
}
