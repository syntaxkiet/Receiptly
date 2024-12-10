using Receiptly.Test.ReceiptProcessing.Resources;
using Shared.Service;
using Shared.Service.Ocr.Tesseract;

namespace Receiptly.Test.OcrTests;

public class OcrServiceTests
{
    [Fact]
    public async Task TesseractService_ExtractReceiptData_ReturnsExpectedResult()
    {
        // Arrange
        var sut = new TesseractService();
        string expectedResult = TestResourceHelper.OcrResultOfReceiptSample1;
        //Todo
        //replace hardcoded image path 
        string imagePath = TestResourceHelper.ReceiptSample1Path;
        string tessdataPath = TestResourceHelper.TessDataPath;
        using var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

        // Act
        //ToDO
        //Replace hardcoded path to tessdata folder
        string result = await sut.ExtractReceiptDataAsync(imageStream, "");

        // Assert
        Assert.Equal(expectedResult, result);
    }
}