using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Interface;
using Shared.Service;
namespace ReceiptlyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OCRController : ControllerBase
    {
        public OCRController(IOCRService oCRService )
        {
            OCRService = oCRService;
        }

        public IOCRService OCRService { get; }

     [HttpPost("extractreceiptdata")]
    public async Task<IActionResult> ExtractReceiptData([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }
        using var stream = file.OpenReadStream();
        await OCRService.ExtractReceiptDataAsync(stream, file.FileName);
        return Ok("File processed successfully.");
    }     
    }
}