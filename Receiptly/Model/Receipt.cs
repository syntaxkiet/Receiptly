namespace Receiptly.Model
{
    public class Receipt
    {
        public int Id { get; set; }
        public string ReceiptName { get; set; }
        public DateTime PurchaseDate { get; set; }
    
        public List<Article> Articles { get; set; } = new List<Article>(); 
    }
}