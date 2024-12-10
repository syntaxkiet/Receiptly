using Receiptly.Interface;
using Moq;
using Receiptly.Service;
using Shared.Models;

namespace Receiptly.Test;

public class ExpiryCheckServiceTests
{
    [Fact]
    public async Task Notify_WhenArticleIsExpired()
    {
        //Arrange
        var mockNotificationService = new Mock<INotificationService>();
        var service = new ExpiryCheckService(mockNotificationService.Object);
        var items = new List<Item>
        {
            new Item
            {
                Id = 1,
                Name = "Test",
                BestBeforeDate = DateTime.Now.AddSeconds(-1),
                Quantity = 1
            }
        };
        
        //Act
        await service.CheckExpiryDateAsync(items);
        
        //Assert
        mockNotificationService.Verify(n => n.NotifyAsync(It.IsAny<string>()), Times.Once);
        
    }
}