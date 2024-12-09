using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Service
{
    public static class JsonHelper
    {
        public static async Task<List<Receipt>?> SafeReadExtractReceiptListFromJson(HttpContent content)
        {
            try
            {
                var receipts = await content.ReadFromJsonAsync<List<Receipt>>();
                return receipts;
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error during deserialization: {ex.Message}");
            }
            return null;
        }

        public static async Task<Receipt?> SafeExtractReceiptFromJson(HttpContent content)
        {
            try
            {
                return await content.ReadFromJsonAsync<Receipt>();
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine($"JSON deserialization failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error during deserialization: {ex.Message}");
            }
            return null;
        }
    }
}
