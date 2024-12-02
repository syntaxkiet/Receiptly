namespace Receiptly.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string ArticleName { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int ReceiptId { get; set; } 
    }
}