using Shared.Interface;
using Shared.Models;

namespace Shared.Service;

public class AddReceiptLogic
{
    
    private readonly IReceiptService _receiptService;
    public Receipt newReceipt { get; set; }

    public AddReceiptLogic(IReceiptService receiptService)
    {
        _receiptService = receiptService;
        newReceipt = new Receipt
        {
            StoreName = "",
            PurchaseDate = DateTime.Today,
            Items = new List<Item>()
        };
    }
    public void LoadReceipt(int receiptId)
    {
        var receipt = _receiptService.GetReceiptById(receiptId);
        if (receipt != null)
        {
            newReceipt = receipt;
        }
    }



    public async Task SaveReceiptAsync()
    {
        if (string.IsNullOrWhiteSpace(newReceipt.StoreName))
        {
            throw new ArgumentException("Butikens namn krävs!");
        }
        // Simulera en API-anrop för att spara kvitto
        await _receiptService.SaveReceiptAsync(newReceipt);
    }

    public void AddItem()
    {
        newReceipt.Items.Add(new Item());
    }
    public void RemoveItem(Item item)
    {
        newReceipt.Items.Remove(item);  
    }
}
