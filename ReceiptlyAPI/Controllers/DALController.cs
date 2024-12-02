using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceiptlyAPI.Data;
using Shared.Models;

namespace ReceiptlyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DALController : ControllerBase
{
    private readonly ReceiptlyDbContext _context;

    public DALController(ReceiptlyDbContext context)
    {
        _context = context;
    }
    [HttpPost("SetToDatabase")]
    public async Task<IActionResult> SetToDatabase(string receiptJson)
    {
        Receipt receipt = new Receipt();
        try
        { 
            receipt = JsonSerializer.Deserialize<Receipt>(receiptJson);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (receipt != null)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Okay");
            
        }
        else
        {
            return BadRequest();
        }
    }
}