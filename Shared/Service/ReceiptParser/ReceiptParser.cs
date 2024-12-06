using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
    public class ReceiptParser
    {

        public static List<ReceiptItem> ExtractItems(string receipt)
        {
            var items = new List<ReceiptItem>();
            string pattern = @"(?<Name>[A-ZÅÄÖa-zåäö\s]+?)\s(?<Quantity>\d*[,\.]?\d+)?\s?[A-Za-z]*\s?(?<UnitPrice>\d+[,\.]?\d+)?\s?(?<TotalPrice>\d+[,\.]?\d+)";
            var matches = Regex.Matches(receipt, pattern);

            foreach (Match match in matches)
            {
                var item = new ReceiptItem
                {
                    Name = match.Groups["Name"].Value.Trim(),
                    Quantity = decimal.TryParse(match.Groups["Quantity"].Value, out var quantity) ? quantity : (decimal?)null,
                    UnitPrice = decimal.TryParse(match.Groups["UnitPrice"].Value, out var unitPrice) ? unitPrice : (decimal?)null,
                    TotalPrice = decimal.TryParse(match.Groups["TotalPrice"].Value, out var totalPrice) ? totalPrice : 0
                };

                items.Add(item);
            }

            return items;
        }
        public class ReceiptItem
        {
            public string Name { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
        }

    }
}
