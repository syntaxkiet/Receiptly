
using Shared.Interface;
using Shared.Receiptly.Models;

namespace Shared.Service;

public class ReceiptService : IReceiptService
{

    public List<Receipt> GetAllReceipts()
    {
        // Return mock data for now
        return MockData.receiptList;
    }
    public Receipt? GetReceiptById(int id)
    {
        // Fetch receipt by ID
        return MockData.receiptList.FirstOrDefault(r => r.Id == id);
    }
}