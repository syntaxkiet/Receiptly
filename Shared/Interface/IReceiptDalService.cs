using Shared.Models;

namespace Shared.Interface;

public interface IReceiptDalService
{
    Task<List<Receipt>> GetAllReceipts(HttpClient? http);
    Task<Receipt?> GetReceiptById(int id, HttpClient? http);
    Task SaveReceipts(List<Receipt> receipts, HttpClient http);
    Task DeleteReceiptById(int id, HttpClient http);
    Task UpdateReceipts(List<Receipt> receipts, HttpClient http);
}