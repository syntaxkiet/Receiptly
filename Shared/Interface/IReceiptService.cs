using Shared.Receiptly.Models;

namespace Shared.Interface;

public interface IReceiptService
{
    List<Receipt> GetAllReceipts();
    Receipt? GetReceiptById(int id);
}