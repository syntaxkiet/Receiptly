namespace Shared.Models;

public class Receipt
{
    public int Id { get; set; }
    
    public string StoreName { get; set; }
    
    public DateTime PurchaseDate { get; set; }  
    
    public List<Item> Items { get; set; }   
}