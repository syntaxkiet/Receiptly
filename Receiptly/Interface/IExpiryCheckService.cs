using Receiptly.Model;

namespace Receiptly.Interface
{
    public interface IExpiryCheckService
    {
        Task CheckExpiryDateAsync(List<Article> articles);
    }
}