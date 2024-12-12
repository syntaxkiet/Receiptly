using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Shared.Interface;
using Shared.Service;

namespace ReceiptlyAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private List<Shared.Models.Receipt> receiptList;

        public NotificationsController(List<Shared.Models.Receipt> receiptList)
        {
            this.receiptList = receiptList;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications(CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;

            while (!cancellationToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var notifications = new List<string>();

                foreach (var receipt in receiptList)
                {
                    foreach (var item in receipt.Items)
                    {
                        if (item.BestBeforeDate <= now && item.BestBeforeDate != null)
                        {
                            notifications.Add($"{item.Name} has expired!");
                            item.BestBeforeDate = null;
                        }
                    }
                }

                if (notifications.Any())
                {
                    var notificationsMessage = string.Join("\n", notifications);
                    var encodedMessage = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(notificationsMessage));
                    return Ok(encodedMessage);
                }
                

                if ((DateTime.Now - startTime).TotalSeconds > 10)
                {
                    return NoContent();
                }
                await Task.Delay(1000, cancellationToken);
            }

            return NoContent();
        }
    }
}