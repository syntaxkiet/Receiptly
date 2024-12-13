using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Shared.Interface;

public interface IReceiptDalService
{
    Task<List<Receipt>?> GetAllReceiptsAsync(HttpClient http);
    Task<Receipt?> GetReceiptByIdAsync(int id, HttpClient http);
    Task<List<Item>?> GetItemsFromReceiptIdAsync(int id, HttpClient http);
    Task CreateReceiptsAsync(List<Receipt> receipts, HttpClient http);
    Task DeleteReceiptByIdAsync(int id, HttpClient http);
    Task UpdateReceiptsAsync(List<Receipt> receipts, HttpClient http);
    Task SaveReceiptAsync(Receipt receipt, HttpClient http);
}