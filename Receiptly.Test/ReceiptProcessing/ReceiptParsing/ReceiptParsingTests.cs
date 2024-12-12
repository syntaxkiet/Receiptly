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
        public new void ExtractReceiptItemLines() => base.ExtractReceiptItemSingleLines();
        public new void ExtractPurchaseDate() => base.ExtractPurchaseDate();
        public new void CombineDualLines() => base.CombineDualLines();
        public new void CombineAndInsertKeyBeforeValue() => base.CombineAndInsertKeyBeforeValue();
        public new void ExtractReceiptStore() => base.ExtractReceiptStore();
        public new void RemoveNonItemLines() => base.RemoveNonItemLines();
        public new void ExtractReceiptItems() => base.ExtractReceiptItems();
    }

    public class ReceiptParserTests
    {
        [Fact]
        public void ParseReceiptFromImageText_ReturnsReceipt_WhenValidDataProvided()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser();

            // Act
            var receipt = parser.ParseReceiptFromImageText(receiptText, ReceiptProcessingTestResourceHelper.TestModel);

            // Assert
            Assert.NotNull(receipt);
            Assert.Equal(new DateTime(2024, 10, 25, 15, 47, 55), parser.ExtractedReceipt.PurchaseDate);
            Assert.NotEmpty(parser.ReceiptLines);
        }

        [Fact]
        public void ExtractReceiptLinesSplitsTextIntoLinesCorrectly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel,
                ReceiptText = $@"{receiptText}"
            };

            // Act
            parser.ExtractReceiptLines();

            // Assert

        }

        [Fact]
        public void ExtractReceiptItemLinesParsesItemsCorrectly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            parser.ExtractReceiptItemLines();

            // Assert

            Assert.Equal(parser.ExtractedReceipt.Items.Count, ReceiptProcessingTestResourceHelper.ExpectedCountOfEctractReceiptItemlines);
        }

        [Fact]
        public void ExtractReceiptItemLinesParsesItemLinesCorrectly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptPatterns.TestModel,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            parser.ExtractReceiptItems();

            // Assert

            Assert.Equal(parser.ExtractedReceipt.Items.Count, ReceiptProcessingTestResourceHelper.ExpectedCountOfEctractReceiptItemlines);
        }

        [Fact]
        public void ExtractReceiptStoreDetectsStoreNameCorrectly()
        {
            // Arrange
            var receiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            var parser = new TestableReceiptParser()
            {
                ParseModel = ReceiptProcessingTestResourceHelper.TestModel,
                ReceiptText = $@"{receiptText}"
            };
            parser.ExtractReceiptLines();

            // Act
            parser.ExtractReceiptStore();

            // Assert
            Assert.Equal(ReceiptProcessingTestResourceHelper.ExpectedResultOfExtractReceiptStoreDetectsStoreNameCorrectly, parser.ExtractedReceipt.StoreName);
        }

        [Fact]
        public void ExtractPurchaseDateParsesDateCorrectly()
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
            Assert.Equal(ReceiptProcessingTestResourceHelper.ExpetedResultOfExtractPurchaseDateParsesDateCorrectl, parser.ExtractedReceipt.PurchaseDate);
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

        [Fact]
        public void CombineAndInsertKeyBeforeValue_ShouldCombineAndReorderLines()
        {
            // Arrange
            var parser = new TestableReceiptParser();
            parser.ParseModel = ReceiptProcessingTestResourceHelper.TestModel;
            parser.ReceiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            parser.ExtractReceiptLines();
            parser.ExtractReceiptItemLines();
            parser.RemoveNonItemLines();

            // Act

            parser.CombineAndInsertKeyBeforeValue();

            // Assert
            var expectedLines = ReceiptProcessingTestResourceHelper.ExpectedOutcomeOfCombineAndInsertKeyBeforeValue;

            Assert.Equal(expectedLines, parser.ReceiptLines);
        }
        [Fact]
        public void RemoveNonItemLinesRemovesExpectedLines()
        {
            // Arrange
            var parser = new TestableReceiptParser();
            parser.ParseModel = ReceiptProcessingTestResourceHelper.TestModel;
            parser.ReceiptText = ReceiptProcessingTestResourceHelper.OcrResultOfReceiptSample1;
            parser.ExtractReceiptLines();
            parser.CombineAndInsertKeyBeforeValue();
            parser.CombineDualLines();
            parser.ExtractReceiptItemLines();
            parser.RemoveNonItemLines();

            // Act

            parser.CombineAndInsertKeyBeforeValue();

            // Assert
            var expectedLines = ReceiptProcessingTestResourceHelper.ExpectedCountOfItemlinesAterREmoveNonItemLines;

            Assert.Equal(expectedLines, parser.ItemLines.Count);
        }
    }
}
