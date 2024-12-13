using Shared.Models;

namespace Shared.Interface
{
    public interface ILocalReceiptService
    {
        Task<List<Receipt>> GetAllReceiptsAsync();
        Task<Receipt?> GetReceiptByIdAsync(int id);
        Task<List<Item>> GetItemsFromReceiptIdAsync(int id);
        Task CreateReceiptsAsync(List<Receipt> receipts);
        Task DeleteReceiptByIdAsync(int id);
        Task UpdateReceiptsAsync(List<Receipt> receipts);
    }

    
}