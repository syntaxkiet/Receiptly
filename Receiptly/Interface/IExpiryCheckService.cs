

namespace Receiptly.Interface
{
    public interface IExpiryCheckService
    {
        Task CheckExpiryDateAsync(List<Shared.Models.Item> items);
    }
}