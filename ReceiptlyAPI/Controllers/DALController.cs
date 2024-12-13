using Microsoft.AspNetCore.Mvc;
using ReceiptlyAPI.Services;
using Shared.DTO;
using Shared.Models;
using System.Text.Json;

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
    public async Task<ActionResult<List<Receipt?>>> GetReceipts()
    {
        var receipts = await _receiptService.GetReceiptsAsync();
        if (receipts == null || receipts.Count <= 0)
        {
            await _receiptService.AddOrUpdateReceiptAsync(MockReceipts.receiptList);
            receipts = await _receiptService.GetReceiptsAsync();
            if((receipts == null || receipts.Count <= 0))
                return NoContent();
        }
        if (receipts != null)
        {
            return Json(receipts);
        }
        return BadRequest("No receipts found");
    }

    [HttpGet("getitemsfromreceiptid")]
    public async Task<ActionResult<List<Item>?>> GetItemsFromReceiptID(int receiptID) 
    {
        return await _receiptService.GetItemsFromReceiptIdAsync(receiptID);
    }


    [HttpPost("createorupdatereceipt")]
    public async Task<ActionResult> CreateOrUpdateReceipt([FromBody] JsonElement jsonElement)
    {
        return Ok(new { message = "Receipts have been created/updated successfully." });
    }

    [HttpPost("savereceipt")]
    public async Task<ActionResult> SaveReceipt(ReceiptApiDto dto)
    {
        return Ok(new { message = "Receipts have been created/updated successfully." });
    }

    [HttpGet("getreceiptsfromid")]
    public async Task<ActionResult> GetReceiptFromID(int id)
    {
        var receipt = await _receiptService.GetReceiptByIdAsync(id);
        if (receipt is Receipt)
        {
            return Ok(receipt);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("deletereceiptbyid")]
    public async Task<IActionResult> DeleteReceipt(int id)
    {
        await _receiptService.DeleteReceiptAsync(id);
        return NoContent();
    }
}