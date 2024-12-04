using Receiptly.Interface;

namespace Receiptly.Service
{
    public class ExpiryCheckService : IExpiryCheckService
    {
        private readonly INotificationService _notificationService;

        public ExpiryCheckService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task CheckExpiryDateAsync(List<Shared.Models.Item> items)
        {
            foreach (var item in items)
            {
                if (item.BestBeforeDate <= DateTime.Now)
                {
                    await _notificationService.NotifyAsync($"Article '{item.Name}' has expired!");
                }
            }
        }
    }
}