using Shared.Interface;
using Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shared.Service
{
    public class ReceiptDalService : IReceiptDalService
    {
        public async Task DeleteReceiptByIdAsync(int id, HttpClient http)
        {
            await http.DeleteAsync($"deletereceiptbyid/{id}");
        }

        public async Task<List<Receipt>?> GetAllReceiptsAsync(HttpClient http)
        {

            var response = await http.GetAsync($"dal/getallreceipts");
            if (response.IsSuccessStatusCode)
            {
                return await JsonHelper.SafeReadExtractReceiptListFromJson(response.Content);
            }
            return null;
        }

        public async Task<Receipt?> GetReceiptByIdAsync(int id, HttpClient http)
        {
            var response = await http.GetAsync($"dal/getreceiptbyid/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await JsonHelper.SafeExtractReceiptFromJson(response.Content);
            }
            return null;
        }


        public async Task CreateReceiptsAsync(List<Receipt> receipts, HttpClient http)
        {
            await http.PostAsJsonAsync($"dal/createorupdatereceipts", receipts);
        }

        public async Task UpdateReceiptsAsync(List<Receipt> receipts, HttpClient http)
        {
            await http.PostAsJsonAsync($"dal/createorupdatereceipts", receipts);
        }
    }
}
