using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Receiptly.Pages;
using Shared.Interface;
using Shared.Models;
using Shared.Service;

namespace Receiptly.Test.IntegrationTest;

public class Receiptlybuttontests
{
    [Fact]
    public void SaveReceiptButton_Should_Call_LogicSaveReceiptAsync()
    {
        //Arrange
        using var ctx = new TestContext();

        var mockReceiptService = new Mock<IReceiptService>();
        mockReceiptService.Setup(s => s.SaveReceiptAsync(It.IsAny<Receipt>()))
            .Returns(Task.CompletedTask);
        
        ctx.Services.AddSingleton<IReceiptService>(mockReceiptService.Object);
        ctx.Services.AddTransient<AddReceiptLogic>(provider =>
        {
            var receiptService = provider.GetRequiredService<IReceiptService>();
            return new AddReceiptLogic(receiptService);
        });
        
        var sut = ctx.Render<AddReceipt>();
        //Must have a StoreName or the test will fail,it¨s requered
        sut.Find("#StoreName").Change("Ica");

        //Act
        sut.Find("button.btn-success").Click();
        
        //Assert
        mockReceiptService.Verify(s => s.SaveReceiptAsync(It.IsAny<Receipt>()), Times.Once);
        
    }
    
}