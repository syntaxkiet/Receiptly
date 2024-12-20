﻿using Moq;
using Shared.Interface;
using Shared.Models;
using Shared.Service;
using Xunit;

namespace Receiptly.Test;

public class ReceiptServiceTests
{
    [Fact]
    public void GettAllReceipts_ShouldReturnAllReceipts()
    {
        //Arrange
        var mockService = new Mock<IReceiptService>();
        mockService.Setup(m => m.GetAllReceipts()).Returns(MockData.receiptList);
     
        //Act
       var result = mockService.Object.GetAllReceipts();
        //Assert
      Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetReceiptById_ShouldReturnCorrectStoreName()
    {
        //Arrange
        var mockService = new Mock<IReceiptService>();
        mockService.Setup(m => m.GetReceiptById(1)).Returns(MockData.receiptList.FirstOrDefault(r => r.Id == 1));
        //Act
        var result = mockService.Object.GetReceiptById(1);
        //Assert
        Assert.Equal("Hemköp", result.StoreName);
    }

    [Fact]
    public void GetAllReceipts_ShouldReturnEmptyList_WhenNoReceiptsExist()
    {
        // Arrange
        var mockService = new Mock<IReceiptService>();
        mockService.Setup(m => m.GetAllReceipts()).Returns(new List<Receipt>());

        // Act
        var result = mockService.Object.GetAllReceipts();

        // Assert
        Assert.Empty(result);
    }
}