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
    public async Task<string> ExtractReceiptDataAsync(Stream fileStream, string tessDataPath)
    {
        string tessdataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tessDataPath);
        Environment.SetEnvironmentVariable("TESSDATA_PREFIX", tessdataPath);

        try
        {
            using var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default);
            Console.WriteLine("Tesseract initialized successfully!");
            // Load the image from the stream using ImageSharp
            using var image = await Image.LoadAsync<Rgba32>(fileStream);

            // Convert ImageSharp image to a format Tesseract can process
            using var ms = new MemoryStream();
            await image.SaveAsPngAsync(ms);
            ms.Position = 0;
            using var pix = Pix.LoadFromMemory(ms.ToArray());

            // Perform OCR
            using var page = engine.Process(pix);
            string extractedText = page.GetText();
            return extractedText;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
    }
}
