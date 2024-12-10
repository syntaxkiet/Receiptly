using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
    internal class ReceiptParserV2
    {
        //        private List<Regex> ItemPatterns { get; set; } = new List<Regex>()
        //{
        //    new Regex(@"(?<Name>[A-ZÅÄÖa-zåäö\s]+?)\s(?<Quantity>\d*[,\.]?\d+)?\s?[A-Za-z]*\s?(?<UnitPrice>\d+[,\.]?\d+)?\s?(?<TotalPrice>\d+[,\.]?\d+)", RegexOptions.IgnoreCase)
        //};
        //private List<Regex> StorePatterns { get; set; } = new List<Regex>();
        //public List<Receipt?>? ExtractReceiptFromImageText(string receiptmageText)
        //{
        //    var store =


        //    return null;
        //}

        //private List<Item?> ExtractReceiptItems(Regex regex, string receiptText)
        //{
        //    var items = new List<Item>();
        //    var matches = regex.Matches(receiptText);
        //    foreach (Match match in matches)
        //    {
        //        var item = new Item
        //        {
        //            Name = match.Groups["Name"].Value.Trim(),
        //        };
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //private List<string> ExtractReceiptStore(Regex regex, string receiptText)
        //{
        //    var stores = new List<string>();
        //    var matches = regex.Matches(receiptText);
        //    foreach (Match match in matches)
        //    {

        //        items.Add(item);
        //    }
        //    return items;
        //}
    }
}
