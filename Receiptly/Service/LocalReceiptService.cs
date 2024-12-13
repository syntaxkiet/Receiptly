using Shared.Interface;
using Shared.Models;

namespace Shared.Service
{
    public class LocalReceiptService : ILocalReceiptService
    {
        private readonly IReceiptDalService _receiptDalService;
        private readonly HttpClient _httpClient;

        private List<Receipt>? _cachedReceipts;

        public LocalReceiptService(IReceiptDalService receiptDalService, HttpClient httpClient)
        {
            _receiptDalService = receiptDalService;
            _httpClient = httpClient;
        }

        public async Task<List<Receipt>> GetAllReceiptsAsync()
        {
            if (_cachedReceipts == null || !_cachedReceipts.Any())
            {
                _cachedReceipts = await _receiptDalService.GetAllReceiptsAsync(_httpClient) ?? new List<Receipt>();
            }

            return _cachedReceipts;
        }

        public async Task<Receipt?> GetReceiptByIdAsync(int id)
        {
            var allReceipts = await GetAllReceiptsAsync();
            return allReceipts.FirstOrDefault(r => r.Id == id);
        }

        public async Task<List<Item>> GetItemsFromReceiptIdAsync(int id)
        {
            var receipt = await GetReceiptByIdAsync(id);
            return receipt?.Items ?? new List<Item>();
        }

        public async Task CreateReceiptsAsync(List<Receipt> receipts)
        {
            await _receiptDalService.CreateReceiptsAsync(receipts, _httpClient);
            _cachedReceipts = null; // Invalidate cache to refresh next time
        }

        public async Task DeleteReceiptByIdAsync(int id)
        {
            await _receiptDalService.DeleteReceiptByIdAsync(id, _httpClient);
            _cachedReceipts = _cachedReceipts?.Where(r => r.Id != id).ToList();
        }

        public async Task UpdateReceiptsAsync(List<Receipt> receipts)
        {
            await _receiptDalService.UpdateReceiptsAsync(receipts, _httpClient);
            _cachedReceipts = null; // Invalidate cache to refresh next time
        }
    }
}