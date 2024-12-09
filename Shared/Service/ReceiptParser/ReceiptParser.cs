using Shared.Models;
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

        public List<Item> ExtractReceiptItems(string receipt)
        {
            var items = new List<Item>();
            string pattern = @"(?<Name>[A-ZÅÄÖa-zåäö\s]+?)\s(?<Quantity>\d*[,\.]?\d+)?\s?[A-Za-z]*\s?(?<UnitPrice>\d+[,\.]?\d+)?\s?(?<TotalPrice>\d+[,\.]?\d+)";
            var matches = Regex.Matches(receipt, pattern);

            foreach (Match match in matches)
            {
                var item = new Item
                {
                    Name = match.Groups["Name"].Value.Trim(),
                };

                items.Add(item);
            }

            return items;
        }
    }
}
