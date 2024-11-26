namespace Shared.Receiptly.Models;

public class MockData
{
    public static List<Receipt> GetMockReceipt()
    {
        return new List<Receipt>
        {
            new Receipt{
            Id = 1,
            StoreName = "Hemköp Gallerian",
            PurchaseDate = new DateTime(2024, 10, 25, 15, 47, 55),
            Items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "MJÖLK 3% 1L",
                    Quantity = 1,
                    
                },
                new Item
                {
                    Id = 2,
                    Name = "Bröd",
                    Quantity = 1,
                    
                },
                new Item
                {
                    Id = 3,
                    Name = "Ost",
                    Quantity = 1,
                  
                },
                new Item
                {
                    Id = 4,
                    Name = "JULMUST SOCKERFRI 1.4L",
                    Quantity = 1,
                    
                },
                new Item
                {
                    Id = 5,
                    Name = "Ägg",
                    Quantity = 12,
                    
                }
                }
            }

        };
            
    }
}