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
        public class ReceiptDataRequest
        {
            public string FileName { get; set; }
            public string FileContent { get; set; }
            public string ContentType { get; set; }
        }

        [HttpPost("ocr/extractreceiptdata")]
        public async Task<IActionResult> ExtractReceiptData([FromBody] ReceiptDataRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.FileContent))
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                // Decode the Base64 file content
                var fileBytes = Convert.FromBase64String(request.FileContent);
                using var stream = new MemoryStream(fileBytes);

                // TODO: Set up dynamic path to tessdata folder and pass
                await OCRService.ExtractReceiptDataAsync(stream, "");

                return Ok("File processed successfully.");
            }
            catch (FormatException ex)
            {
                return BadRequest($"Invalid Base64 string: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        
        
    //  [HttpPost("extractreceiptdata")]
    // public async Task<IActionResult> ExtractReceiptData([FromForm] IFormFile file)
    // {
    //     if (file == null || file.Length == 0)
    //     {
    //         return BadRequest("No file uploaded.");
    //     }
    //     using var stream = file.OpenReadStream();
    //     //ToDo 
    //     //Set up dynamic path to tessdata folder and pass
    //     await OCRService.ExtractReceiptDataAsync(stream, "");
    //     return Ok("File processed successfully.");
    // }     
    }
}
