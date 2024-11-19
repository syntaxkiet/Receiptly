using System.Security.Cryptography.X509Certificates;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Receiptly.Models
{
    public static class MockData
    {
        public static Receipt GetSampleReceipt()
        {
            return new Receipt
            {
               StoreName = "Ica Kvantum",
               Items = new List<Item>
               {
                   new Item { Name = "Mjölk 3% (1L)", Quantity = 2 },
                   new Item { Name = "Bröd", Quantity = 1 },
                   new Item { Name = "bregott 350g", Quantity = 1 },
               }
            };
        }
    }
}
