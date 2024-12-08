using Microsoft.AspNetCore.Mvc;
using ReceiptlyAPI.Services;
using Shared.Models;

namespace ReceiptlyAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ReceiptController : ControllerBase
{
    private readonly ReceiptService _receiptService;

    public ReceiptController(ReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts()
    {
        var receipts = await _receiptService.GetReceiptsAsync();
        return Ok(receipts);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrUpdateReceipt(List<Receipt> receipts)
    {
        await _receiptService.AddOrUpdateReceiptAsync(receipts);
        return Ok(new { message = "Receupts have been created/updated successfully." });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReceipt(int id)
    {
        await _receiptService.DeleteReceiptAsync(id);
        return NoContent();
    }
}