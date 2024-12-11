using Shared.Models;
using Shared.Service.ReceiptParser;
using Receiptly.Test.ReceiptProcessing.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Receiptly.Test.ReceiptProcessing.Tests
{
    public class TestableReceiptParser : ReceiptParser
    {
        public new void ExtractReceiptLines() => base.ExtractReceiptLines();
        public new bool ExtractReceiptItems() => base.ExtractReceiptItems();
        public new void ExtractPurchaseDate() => base.ExtractPurchaseDate();
        public new void CombineDualLines() => base.CombineDualLines();
    }

    public class ReceiptParserTests
    {
        [Fact]
        public void ParseReceiptFromImageText_ReturnsReceipt_WhenValidDataProvided()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };

            // Act
            var receipt = parser.ParseReceiptFromImageText(receiptText);

            // Assert
            Assert.NotNull(receipt);
            Assert.Equal(new DateTime(2024, 10, 25, 15, 47, 55), parser.ExtractedReceipt.PurchaseDate);
            Assert.NotEmpty(parser.ReceiptLines);
        }

        [Fact]
        public void ExtractReceiptLines_SplitsTextIntoLines_Correctly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };

            // Act
            parser.ExtractReceiptLines();

            // Assert

        }

        [Fact]
        public void ExtractReceiptItems_ParsesItems_Correctly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            var itemsExtracted = parser.ExtractReceiptItems();

            // Assert
            Assert.True(itemsExtracted);
            Assert.NotNull(parser.ExtractedReceipt);
        }

        [Fact]
        public void ExtractReceiptStore_DetectsStoreName_Correctly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            parser.ExtractPurchaseDate();

            // Assert
            Assert.Equal(new DateTime(2024, 10, 25, 15, 47, 55), parser.ExtractedReceipt.PurchaseDate);
        }

        [Fact]
        public void ExtractPurchaseDate_ParsesDate_Correctly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            parser.ExtractPurchaseDate();

            // Assert
            Assert.Equal(new DateTime(2024, 10, 25, 15, 47, 55), parser.ExtractedReceipt.PurchaseDate);
        }

        [Fact]
        public void CombineDualLines_ShouldCombineLinesBasedOnPatterns()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel
                ,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();
            parser.CombineDualLines();

            Assert.Equal(ReceiptProcessingTestResourceHelper.ExpectedSeparatedCombinedStringsList, parser.ReceiptLines);
        }
    }
}
