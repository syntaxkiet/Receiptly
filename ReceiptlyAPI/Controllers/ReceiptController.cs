using Microsoft.AspNetCore.Mvc;
using ReceiptlyAPI.Services;
using Shared.Models;

namespace ReceiptlyAPI.Controllers;


[ApiController]
[Route("dbaccess")]
public class ReceiptController : Controller
{
    private readonly DbAccess _receiptService;

    public ReceiptController(DbAccess receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet("getallreceipt")]
    public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts()
    {
        var receipts = await _receiptService.GetReceiptsAsync();
        return Ok(receipts);

    }

    [HttpPost("createorupdatereceipt")]
    public async Task<ActionResult> CreateOrUpdateReceipt(List<Receipt> receipts)
    {
        await _receiptService.AddOrUpdateReceiptAsync(receipts);
        return Ok(new { message = "Receipts have been created/updated successfully." });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReceipt(int id)
    {
        await _receiptService.DeleteReceiptAsync(id);
        return NoContent();
    }
}