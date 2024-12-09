namespace Shared.Models;

public class MockData
{
    public static List<Receipt> receiptList { get; set; } = new List<Receipt>
    {
        new Receipt
        {
            Id = 1,
            StoreName = "Hemköp",
            PurchaseDate = DateTime.Now.AddDays(-5),
            Items = new List<Item>
            {
                new Item { Id = 1, Name = "Mjölk", Quantity = 1, BestBeforeDate = null, ReceiptId = 1 },
                new Item { Id = 2, Name = "Bröd", Quantity = 2, BestBeforeDate = DateTime.Now.AddDays(5), ReceiptId = 1 },
                new Item { Id = 3, Name = "Ägg", Quantity = 1, BestBeforeDate = DateTime.Now.AddDays(10), ReceiptId = 1 }
            }
        },
        new Receipt
        {
            Id = 2,
            StoreName = "ICA",
            PurchaseDate = DateTime.Now.AddDays(-3),
            Items = new List<Item>
            {
                new Item { Id = 4, Name = "Smör", Quantity = 1, BestBeforeDate = null, ReceiptId = 2 },
                new Item { Id = 5, Name = "Ost", Quantity = 1, BestBeforeDate = DateTime.Now.AddDays(7), ReceiptId = 2 }
            }
        }
    };
}
