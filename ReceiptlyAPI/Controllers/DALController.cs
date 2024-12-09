using Microsoft.AspNetCore.Mvc;
using ReceiptlyAPI.Services;
using Shared.Models;

namespace ReceiptlyAPI.Controllers;


[ApiController]
[Route("dal")]
public class DALController : Controller
{
    private readonly DbAccess _receiptService;

    public DALController(DbAccess receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet("getallreceipts")]
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

    [HttpGet]

    [HttpDelete]
    public async Task<IActionResult> DeleteReceipt(int id)
    {
        await _receiptService.DeleteReceiptAsync(id);
        return NoContent();
    }
}