using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Receiptly.Interface;
using Receiptly.Service;
using Shared.Interface;
using Shared.Service;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddScoped<IReceiptDalService, ReceiptDalService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddTransient<AddReceiptLogic>(provider =>
            {
                var receiptService = provider.GetRequiredService<IReceiptService>();
                return new AddReceiptLogic(receiptService);
            });
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44338/") });
            await builder.Build().RunAsync();
        }
    }
}
