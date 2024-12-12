using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shared.Interface;
using Shared.Models;
using Shared.Service;
using Shared.Service.ReceiptParser;
namespace ReceiptlyAPI.Controllers
{
    [ApiController]
    [Route("ocr")]
    public class OCRController : Controller
    {
        public OCRController(IOCRService oCRService, IReceiptStringParser receiptStringParser)
        {
            OCRService = oCRService;
            ReceiptStringParser = receiptStringParser;
        }

        private IOCRService OCRService { get; set; }
        public IReceiptStringParser ReceiptStringParser { get; }

        public class ReceiptDataRequest
        {
            public string FileName { get; set; }
            public string FileContent { get; set; }
            public string ContentType { get; set; }
        }

        [HttpPost("extractreceiptdata")]
        public async Task<ActionResult<Receipt>> ExtractReceiptData([FromBody] ReceiptDataRequest request)
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
                var textFromImage = await OCRService.ExtractReceiptDataAsync(stream, "Shared\\Service\\Ocr\\Tesseract\\");
                if (textFromImage != null)
                {
                    //ToDo Set up fetching models from db and load to list of models
                    var result = ReceiptStringParser.ParseReceiptFromImageText(textFromImage, ReceiptPatterns.TestModel);
                    if (result is Receipt)
                    {
                        return Json(result);
                    }
                }
            }
            catch (FormatException ex)
            {
                return BadRequest($"Invalid Base64 string: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            return StatusCode(500, $"An error occurred");
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
