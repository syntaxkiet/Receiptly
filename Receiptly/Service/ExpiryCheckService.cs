using Receiptly.Interface;
using Receiptly.Model;

namespace Receiptly.Service
{
    public class ExpiryCheckService : IExpiryCheckService
    {
        private readonly INotificationService _notificationService;

        public ExpiryCheckService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task CheckExpiryDateAsync(List<Article> articles)
        {
            foreach (var article in articles)
            {
                if (article.ExpirationDate <= DateTime.Now)
                {
                    await _notificationService.NotifyAsync($"Article '{article.ArticleName}' has expired!");
                }
            }
        }
    }
}