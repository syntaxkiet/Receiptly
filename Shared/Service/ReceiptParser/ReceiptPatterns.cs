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
            Name = "Hemköp",
            ItemLinePattern = @"\b\d{1,4},\d{2}$",
            StorePattern = @"Hemköp",
            LineSeparatorPattern = @"\n",
            PuchaseDatePattern = @"(?<PurchaseDate>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})",
            DualLinePatterns = new List<string>() { @"\+PANT", @"\d{1,3}[,]\d{2}/kg", @"\d{1,3},\s?\d{2}kr/\w{1,3}" },
            TriggersAtNextIndexInsertsBeforeOnCurrentIndex = new() { { @"(?i)\+PANT(?!\S)", @"\b\d{1,4},\d{2}$" } },
            ExcludeLineFromItems = new List<string>() {@"(?i)\b bonusgrundande\b", @"(?i)\bkontokort\b", @"(?i)\bpoänggrundande\b"}
        };

        public static List<ReceiptParseModel> ParseModels { get; set; } = new()
        {
            new ReceiptParseModel()
            {
                Name = "Hemköp Kungsgatan Eskilstuna",
                ItemLinePattern = @"(?<Name>.+?)\s+\d{1,3}(?:[,.]\d{2})\s*(kr|)?$\r\n",
                StorePattern = "Hemköp",
                LineSeparatorPattern = "/n",
                PuchaseDatePattern = @"(?<PurchaseDate>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}"
            }
        };
    }
}
