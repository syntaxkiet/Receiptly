using Microsoft.JSInterop;
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
    
    [Fact]
    public async Task NotifyAsync_CallsJsRuntimeWithCorrectMessage()
    {
        // Arrange
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var notificationService = new BrowserNotificationService(jsRuntimeMock.Object);

        var expectedMessage = "Test Notification";

        // Act
        await notificationService.NotifyAsync(expectedMessage);

        // Assert
        jsRuntimeMock.Verify(
            js => js.InvokeVoidAsync("notify", It.Is<object[]>(args => (string)args[0] == expectedMessage)),
            Times.Once
        );
    }
    
}