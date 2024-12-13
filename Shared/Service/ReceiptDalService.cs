using Shared.Interface;
using Shared.Models;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<List<Item>?> GetItemsFromReceiptIdAsync(int id, HttpClient http)
        {
            var response = await http.GetAsync($"getitemsfromreceiptid/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await JsonHelper.SafeReadExtractItemListFromJson(response.Content);
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
            try
            {
                // Serialize the object list to JSON using Newtonsoft.Json
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(receipts);

                // Create the HTTP content
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await http.PostAsync("dal/createorupdatereceipts", httpContent);

                // Handle response as needed
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending receipts: {ex.Message}");
                throw; // Rethrow or handle as needed
            }
        }


        public async Task UpdateReceiptsAsync(List<Receipt> receipts, HttpClient http)
        {
            await http.PostAsJsonAsync($"dal/createorupdatereceipts", receipts);
        }
    }
}
