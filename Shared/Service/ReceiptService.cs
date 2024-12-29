using Shared.Interface;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Service;
    public class ReceiptService : IReceiptService
{
   
    private readonly IReceiptDalService _dalService;
    private readonly HttpClient _httpClient;

    public ReceiptService(IReceiptDalService dalService, HttpClient httpClient)
    {
        _dalService = dalService;
        _httpClient = httpClient;
    }

    public List<Receipt> GetAllReceipts()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Receipt>> GetAllReceiptsAsync()
    {
        return await _dalService.GetAllReceiptsAsync(_httpClient) ?? new List<Receipt>();
    }

    public Receipt? GetReceiptById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Receipt?> GetReceiptByIdAsync(int id)
    {
        return await _dalService.GetReceiptByIdAsync(id, _httpClient);
    }

    public async Task SaveReceiptAsync(Receipt receipt)
    {
        await _dalService.CreateReceiptsAsync(new List<Receipt> { receipt }, _httpClient);
    }
}

