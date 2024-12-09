using Shared.Interface;
using Shared.Models;

namespace Shared.Service;

public class ReceiptMockingService :IReceiptService
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
    

    public async Task SaveReceiptAsync(Receipt receipt)
    {
        if (string.IsNullOrWhiteSpace(receipt.StoreName))
        {
            throw new ArgumentException("Butikens namn krävs!");
        }

        // Spara kvittot (simulerat här)
        await Task.Delay(100); // Simulera API-anrop
        Console.WriteLine("Kvitto sparat i databasen!");
        
        MockData.receiptList.Add(receipt);
    }
}