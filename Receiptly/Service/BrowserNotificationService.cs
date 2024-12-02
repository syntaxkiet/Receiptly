using Microsoft.JSInterop;
using Receiptly.Interface;

namespace Receiptly.Service
{
    public class BrowserNotificationService : INotificationService
    {
        private readonly IJSRuntime _jsRuntime;

        public BrowserNotificationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        
        public async Task NotifyAsync(string message)
        {
            // Call the JavaScript function for notifications
            await _jsRuntime.InvokeVoidAsync("notify", message);
        }
    }
}