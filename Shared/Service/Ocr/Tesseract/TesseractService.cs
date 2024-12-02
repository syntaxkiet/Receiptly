using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Interface;
using Tesseract;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Shared.Service
{
    public class TesseractService : IOCRService
    {
        //ToDo
        //If !YAGNI Implement method with input variables for lagnuage and/or enginemode. 
        public async Task<string> ExtractReceiptDataAsync(Stream fileStream, string? tessDataPath)
        {
            //ToDo
            //Break up method in to smaller testable methods
            string tessdataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tessDataPath);
            tessDataPath ??= "./tessdata";
            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", tessdataPath);
            try
            {
                using var engine = new TesseractEngine(@"./tessdata", "swe", EngineMode.Default);
                using var image = await Image.LoadAsync<Rgba32>(fileStream);

                // Convert ImageSharp image to a format Tesseract can process
                using var ms = new MemoryStream();
                await image.SaveAsPngAsync(ms);
                ms.Position = 0;
                using var pix = Pix.LoadFromMemory(ms.ToArray());

                // Perform OCR
                using var page = engine.Process(pix);
                string extractedText = page.GetText();
                extractedText = NormalizeLineBreaks(extractedText);
                return extractedText;
            }
            catch (TesseractException tessEx)
            {
                Console.WriteLine($"Tesseract Error: {tessEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }

        private void ValidateTessdataPath(string tessDataPath)
        {
            if (!Directory.Exists(tessDataPath))
                throw new DirectoryNotFoundException($"Tessdata path not found: {tessDataPath}");
        }
        
        private async Task<Pix> ConvertStreamToPixAsync(Stream fileStream)
        {
            using var image = await Image.LoadAsync<Rgba32>(fileStream);
            using var ms = new MemoryStream();
            await image.SaveAsPngAsync(ms);
            ms.Position = 0;

            return Pix.LoadFromMemory(ms.ToArray());
        }
        
        private TesseractEngine InitializeTesseractEngine(string tessDataPath)
        {
            return new TesseractEngine(tessDataPath, "swe", EngineMode.Default);
        }
        
        private string PerformOcr(TesseractEngine engine, Pix pix, string languageSetting)
        {
            using var page = engine.Process(pix);
            return page.GetText();
        }
        
        public string NormalizeLineBreaks(string input)
        {
            return input.Replace("\n", "\r\n");
        }

    }
}