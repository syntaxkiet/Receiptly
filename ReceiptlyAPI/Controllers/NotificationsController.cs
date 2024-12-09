using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;

namespace ReceiptlyAPI.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private List<Shared.Models.Receipt> mockedData;

        public NotificationsController()
        {
            mockedData = new List<Shared.Models.Receipt>();
        }

        [HttpGet]
        public IActionResult GetNotifications()
        {
            var now = DateTime.Now;
            var notifications = new List<string>();

            foreach (var receipt in mockedData)
            {
                foreach (var item in receipt.Items)
                {
                    if (item.BestBeforeDate <= now)
                    {
                        notifications.Add($"{item.Name} has expired!");
                    }
                }
            }

            if (notifications.Any())
            {
                var notificationsMessage = string.Join(", ", notifications);
                var encodedMessage = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(notificationsMessage));
                return Ok(encodedMessage);
            }

            return NoContent();
        }
    }
}