using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Moq.Protected;
using Shared.Interface;
using Shared.Models;
using Shared.Service;
using System.Net;
using Xunit;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Receiptly.Test;

public class ReceiptServiceTests
{
   
    private readonly Mock<IReceiptDalService> _mockDalService;
    private readonly ReceiptService _sut; // System Under Test
    private readonly HttpClient _mockHttpClient;

    public ReceiptServiceTests()
    {
        _mockDalService = new Mock<IReceiptDalService>();
        _mockHttpClient = new HttpClient(); 

        
        _sut = new ReceiptService(_mockDalService.Object, _mockHttpClient);
    }

    [Fact]
    public async Task GetAllReceipts_ShouldReturnReceipts()
    {
        // Arrange
        var receipts = new List<Receipt>
        {
            new Receipt { Id = 1, StoreName = "Hemköp" },
            new Receipt { Id = 2, StoreName = "ICA" }
        };

        _mockDalService
            .Setup(dal => dal.GetAllReceiptsAsync(It.IsAny<HttpClient>()))
            .ReturnsAsync(receipts);

        // Act
        var result = await _sut.GetAllReceiptsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Hemköp", result[0].StoreName);
    }

    [Fact]
    public async Task GetReceiptById_ShouldReturnCorrectReceipt()
    {
        // Arrange
        var receipt = new Receipt { Id = 1, StoreName = "Hemköp" };

        _mockDalService
            .Setup(dal => dal.GetReceiptByIdAsync(1, It.IsAny<HttpClient>()))
            .ReturnsAsync(receipt);

        // Act
        var result = await _sut.GetReceiptByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Hemköp", result.StoreName);
    }

}