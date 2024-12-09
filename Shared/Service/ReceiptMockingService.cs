using Shared.Interface;
using Shared.Models;

namespace Shared.Service;

public class ReceiptMockingService :IReceiptDalService
{
    public Task DeleteReceiptById(int id, HttpClient? http)
    {
        throw new NotImplementedException();
    }

    public Task<List<Receipt>> GetAllReceipts(HttpClient? http)
    {
                // Return mock data for now
        return Task.FromResult(MockData.receiptList);
    }

    public Task<Receipt?> GetReceiptById(int id, HttpClient? http)
    {
        // Fetch receipt by ID
        return Task.FromResult(MockData.receiptList.Find(r => r.Id == id));
    }
    public Task SaveReceipts(List<Receipt> receipts, HttpClient http)
    {
        throw new NotImplementedException();
    }

    public Task UpdateReceipts(List<Receipt> receipts, HttpClient http)
    {
        throw new NotImplementedException();
    }
}