using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class MockReceipts
    {        
    public static List<Receipt> receiptList { get; set; } = new List<Receipt>
    {
        new Receipt
        {
            Id = 1,
            StoreName = "Willy´s",
            PurchaseDate = DateTime.Now.AddDays(-5),
            Items = new List<Item>
            {
                new Item { Id = 1, Name = "Korv", Quantity = 1, BestBeforeDate = null, },
                new Item { Id = 2, Name = "Kaviar", Quantity = 2, BestBeforeDate = DateTime.Now.AddDays(5)},
                new Item { Id = 3, Name = "Långfil", Quantity = 1, BestBeforeDate = DateTime.Now.AddDays(10)}
            }
        },
        new Receipt
        {
            Id = 2,
            StoreName = "Coop",
            PurchaseDate = DateTime.Now.AddDays(-3),
            Items = new List<Item>
            {
                new Item { Id = 4, Name = "Sardeller", Quantity = 1, BestBeforeDate = null, ReceiptId = 2 },
                new Item { Id = 5, Name = "Fryspizza", Quantity = 1, BestBeforeDate = DateTime.Now.AddDays(7)}
            }
        }
    };
    }
}
