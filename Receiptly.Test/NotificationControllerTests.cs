using Microsoft.AspNetCore.Mvc;
using ReceiptlyAPI.Controllers;
using Moq;
using Shared.Interface;
using Shared.Models;

namespace Receiptly.Test
{
    public class NotificationControllerTests
    {
        private NotificationsController _controller;
        
        [Fact]
        public async Task GetNotification_ReturnNotifications_WhenExpiredItemExists()
        {
            // Arrange
            var mockReceiptService = new Mock<IReceiptDalService>();
            var receiptList = new List<Receipt>
            {
                new Receipt
                {
                    Items = new List<Item>
                    {
                        new Item { Name = "Milk", BestBeforeDate = DateTime.Now.AddSeconds(-1) }, 
                        new Item { Name = "Bread", BestBeforeDate = DateTime.Now.AddMinutes(1) }  
                    }
                }
            };
        
            _controller = new NotificationsController(receiptList);

            // Act
            var result = await _controller.GetNotifications(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var encodedMessage = Assert.IsType<string>(okResult.Value); 
            
            var decodedMessage = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedMessage));
            
            Assert.Contains("Milk has expired!", decodedMessage); 
            Assert.DoesNotContain("Bread has expired!", decodedMessage); 
        }
        
    }
}