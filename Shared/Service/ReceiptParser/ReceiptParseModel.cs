using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
    public class ReceiptParseModel
    {
        public string Name { get; set; } = string.Empty;   
        public string ItemPattern { get; set; } = string.Empty; 
        public string StorePattern { get; set; } = string.Empty;
        public string PuchaseDatePattern { get; set; } = string.Empty;
        public string LinePattern { get; set; } = string.Empty;
        public List<string> DualLinePatterns { get; set; } = new();
    }
}
