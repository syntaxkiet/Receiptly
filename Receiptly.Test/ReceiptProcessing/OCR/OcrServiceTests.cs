using Receiptly.Test.ReceiptProcessing.Resources;
using Shared.Service;
using Shared.Service.Ocr.Tesseract;

namespace Receiptly.Test.ReceiptProcessing.OCR;

public class OcrServiceTests
{
    [Fact]
    public async Task TesseractService_ExtractReceiptData_ReturnsExpectedResult()
    {
        // Arrange
        var sut = new TesseractService();
        string expectedResult = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1.Replace("\\n", "\n");

        //Todo
        //replace hardcoded image path 
        string imagePath = ReceiptProcessingTestResourceHelper.ReceiptSample1Path;
        string tessdataPath = ReceiptProcessingTestResourceHelper.TessDataPath;
        using var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

        // Act
        //ToDO
        //Replace hardcoded path to tessdata folder
        string result = await sut.ExtractReceiptDataAsync(imageStream, "");

        // Assert
        Assert.Equal(expectedResult, result);
    }
}