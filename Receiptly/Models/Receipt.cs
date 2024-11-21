namespace Receiptly.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public string StoreName { get; set; }

        public List<Item> Items { get; set; }
    }
 

    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
