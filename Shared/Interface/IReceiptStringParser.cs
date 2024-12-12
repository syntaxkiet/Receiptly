using Shared.Models;
using Shared.Service.ReceiptParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interface
{
    public interface IReceiptStringParser
    {
        public string ReceiptText { get; set; }
        public List<string> ReceiptLines { get; set; }
        public Receipt ExtractedReceipt { get; set; } 
        public ReceiptParseModel ParseModel { get; set; }
        Receipt? ParseReceiptFromImageText(string ReceiptText, ReceiptParseModel model);
    }
}
