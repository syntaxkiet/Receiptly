namespace Receiptly.Interface
{
    public interface INotificationService
    {
        Task NotifyAsync(string message);
    }
}