namespace Shared.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime? BestBeforeDate { get; set; }
    public int? ReceiptId { get; set; }
    public virtual Receipt? Receipt { get; set; }
}