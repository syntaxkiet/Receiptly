
using Moq;
using Receiptly.Pages;
using Shared.Interface;
using Shared.Models;
using Shared.Service;


namespace Receiptly.Test.ComponentTest;

public class AddReceiptTests
{
 
    [Fact]
   public async Task SaveReceipt_ShouldThrowArgumentException_WhenStoreNameIsEmpty()
    {
        //Arrange
        var mockService = new Mock<IReceiptService>();
        var sut = new AddReceiptLogic(mockService.Object);
        //Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => sut.SaveReceiptAsync());
        //Assert
        Assert.Equal("Butikens namn krävs!", exception.Message);
    }

    [Fact]
    public async Task SaveReceiptAsync_WhenStoreNameIsNotEmpty()
    {
        //Arrange
        var mockService = new Mock<IReceiptService>();
        var sut = new AddReceiptLogic(mockService.Object);
        sut.newReceipt.StoreName = "Ica";
        //Act
        await sut.SaveReceiptAsync();
        //Assert
         mockService.Verify(s => s.SaveReceiptAsync(sut.newReceipt), Times.Once);
    }

    [Fact]
    public void AddItem_ShouldAddNewItem()
    {
        //Arrange
        var mockReceiptService = new Mock<IReceiptService>();
        var sut = new AddReceiptLogic(mockReceiptService.Object);
        //Act
        sut.AddItem();
        //Assert
        Assert.Equal(1,sut.newReceipt.Items.Count);
    }

    [Fact]
    public void RemoveItem_ShouldRemoveItemFromReceipt()
    {
        //Arrange
        var mockReceiptService = new Mock<IReceiptService>();
        var sut = new AddReceiptLogic(mockReceiptService.Object);
        var item = new Item();
        sut.AddItem();
        sut.newReceipt.Items[0] = item;
        //Act
        sut.RemoveItem(item);
        //Assert
        Assert.Empty(sut.newReceipt.Items);

    }
}