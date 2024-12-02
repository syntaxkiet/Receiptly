using Microsoft.AspNetCore.Mvc;

namespace ReceiptlyAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class ConnectionTest : Controller
{
    [HttpGet("checkconnection")]
    public IActionResult CheckConnection()
    {
        return new OkObjectResult("OK");
    }
}