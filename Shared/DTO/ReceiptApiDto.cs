using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ReceiptApiDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public List<ReceiptItemApiDto> Items { get; set; } = new List<ReceiptItemApiDto>();
    }
}
