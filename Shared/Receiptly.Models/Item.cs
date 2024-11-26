namespace Shared.Receiptly.Models;

public class Item
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime? BestBeforeDate { get; set; }
}