using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
    /// <summary>
    /// Collection of typed regex patterns to extract receipt data.
    /// Please note the extracion methods uses named groups <Name><Item><Store><PurchaseDate> which needs to be included for given regex
    /// </summary>
    public static class ReceiptPatterns
    {
        public static ReceiptParseModel TestModel { get; } = new ReceiptParseModel()
        {
            Name = "Hemköp Kungsgatan Eskilstuna",
            ItemPattern = @"(?<Name>.+?)\s+\d{1,3}(?:[,.]\d{2})\s*(kr|)?$",
            StorePattern = "Hemköp",
            LinePattern = @"\n", 
            PuchaseDatePattern = @"(?<PurchaseDate>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})",
            DualLinePatterns = new List<string>(){@"Pant", @"\d{1,3}[,.]\d{2}/kg"}      
        };
        public static List<ReceiptParseModel> ParseModels { get; set; } = new()
        {
            new ReceiptParseModel()
            {
                Name = "Hemköp Kungsgatan Eskilstuna",
                ItemPattern = @"(?<Name>.+?)\s+\d{1,3}(?:[,.]\d{2})\s*(kr|)?$\r\n",
                StorePattern = "Hemköp",
                LinePattern = "/n",
                PuchaseDatePattern = @"(?<PurchaseDate>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}"
            }
        };
    }
}
