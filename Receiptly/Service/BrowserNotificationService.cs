using Receiptly.Interface;

namespace Receiptly.Service
{
    public class BrowserNotificationService : INotificationService
    {
        public async Task NotifyAsync(string message)
        {
            //TODO: Call browser's notification API, figure out why JSRuntime calls don't work
            //await JSRuntime.Current.InvokeAsync("notification", message);
        }
    }
}