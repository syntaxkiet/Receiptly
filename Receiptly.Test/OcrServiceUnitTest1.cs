using Shared.Sample;
using Shared.Service;

namespace Receiptly.Test;

public class OcrServiceUnitTest1
{
    [Fact]
    public async Task OCRServiceTest()
    {
        // Arrange
        var sut = new TesseractService();
        string expectedResult = ReceiptSample.ReceiptString;
        using var imageStream = new FileStream("Shared/Sample/2024-10-25T15_47_57.jpg", FileMode.Open, FileAccess.Read);

        // Act
        string result = await sut.ExtractReceiptDataAsync(imageStream);

        // Assert
    }
}