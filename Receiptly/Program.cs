using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Receiptly.Interface;
using Receiptly.Service;
using Shared.Interface;
using Shared.Service;

namespace Receiptly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped<INotificationService, BrowserNotificationService>();
            builder.Services.AddScoped<IExpiryCheckService, ExpiryCheckService>();
            builder.Services.AddScoped<IReceiptService, ReceiptMockingService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // builder.Services.AddScoped<IOCRService, TesseractService>();
            //         builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7018/") });
            await builder.Build().RunAsync();
        }
    }
}
