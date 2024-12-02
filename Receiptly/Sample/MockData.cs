using Receiptly.Model;

namespace Receiptly.Sample
{
    public static class MockData
    {
        public static List<Receipt> GetMockReceipts()
        {
            return new List<Receipt>
            {
                new Receipt
                {
                    Id = 1,
                    ReceiptName = "Grocery Receipt",
                    PurchaseDate = DateTime.Now.AddDays(-5),
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            Id = 1,
                            ArticleName = "Milk",
                            ExpirationDate = DateTime.Now.AddDays(-1), 
                            ReceiptId = 1
                        },
                        new Article
                        {
                            Id = 2,
                            ArticleName = "Bread",
                            ExpirationDate = DateTime.Now.AddDays(2),
                            ReceiptId = 1
                        }
                    }
                },
                new Receipt
                {
                    Id = 2,
                    ReceiptName = "Electronics Receipt",
                    PurchaseDate = DateTime.Now.AddDays(-30),
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            Id = 3,
                            ArticleName = "Phone Charger",
                            ExpirationDate = DateTime.Now.AddYears(1),
                            ReceiptId = 2
                        },
                        new Article
                        {
                            Id = 4,
                            ArticleName = "Laptop Warranty",
                            ExpirationDate = DateTime.Now.AddMonths(-1), 
                            ReceiptId = 2
                        }
                    }
                }
            };
        }

        public static List<Article> GetMockArticles()
        {
            List<Article> articlesList = new List<Article>();
            
            foreach (Receipt receipt in GetMockReceipts())
            {
                foreach (Article article in receipt.Articles)
                {
                    articlesList.Add(article);
                }
            }

            return articlesList;
        }
    }
}