using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service.ReceiptParser
{
        public class ReceiptItem
        {
            public string Name { get; set; }
            public decimal? Quantity { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
        }
}
