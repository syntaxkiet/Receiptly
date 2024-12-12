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
        public string ItemLinePattern { get; set; } = string.Empty; 
        public string StorePattern { get; set; } = string.Empty;
        public string PuchaseDatePattern { get; set; } = string.Empty;
        public string LineSeparatorPattern { get; set; } = string.Empty;
        //Pattern that inserts recognised line at the end of previous line
        public List<string> DualLinePatterns { get; set; } = new();
        //Identifies and excludes Items that get caught by the  pattern but arent Items
        public List<string> ExcludeLineFromItems {get; set;} = new();
        //Key pattern will grab any next line that matches and insert before value pattern on the current line
        public Dictionary<string,string> TriggersAtNextIndexInsertsBeforeOnCurrentIndex {get; set;}
    }
}
