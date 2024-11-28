using Shared.Sample;
using Shared.Service;

namespace Receiptly.Test;

public class OcrServiceUnitTest1
{
    [Fact]
    public async Task TesseractService_ExtractReceiptData_ReturnsExpectedResult()
    {
        // Arrange
        var sut = new TesseractService();
        string expectedResult = ReceiptSample.ReceiptString;
        //Todo
        //replace hardcoded image path 
        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sample", "2024-10-25T15_47_57.jpg");
        string tessdataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
        using var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

        // Act
        //ToDO
        //Replace hardcoded path to tessdata folder
        string result = await sut.ExtractReceiptDataAsync(imageStream, "");

        // Assert
        Assert.Equal(expectedResult, result);
    }
}