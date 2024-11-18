using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Shared.Interface;
using Tesseract;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Shared.Service
{
    public class TesseractService : IOCRService
    {
    public async Task ExtractReceiptData(Stream fileStream, string fileName)
    {
        // Ensure the 'Sample' directory exists
        string sampleDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sample");
        if (!Directory.Exists(sampleDirectory))
        {
            Directory.CreateDirectory(sampleDirectory);
        }

        // Define the output file path
        string outputFilePath = Path.Combine(sampleDirectory, $"{Path.GetFileNameWithoutExtension(fileName)}.txt");

        // Initialize Tesseract engine
        using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);

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

        // Write the extracted text to the output file
        await File.WriteAllTextAsync(outputFilePath, extractedText);
    }
    }
}
