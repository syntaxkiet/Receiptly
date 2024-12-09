using Shared.Interface;
using Shared.Models;

namespace Shared.Service
{
    public class ReceiptDalService : IReceiptDalService
    {
        public void DeleteReceiptById(int id, HttpClient http)
        {
            throw new NotImplementedException();
        }

        public List<Receipt> GetAllReceipts(HttpClient? http)
        {
            if (http == null)
            {
                return new List<Receipt>();
            }
            else
            {
                var response = http.GetAsync("dal/getallreceipts");
                if(response.)
            }
        }

        public Receipt? GetReceiptById(int id, HttpClient? http)
        {
            throw new NotImplementedException();
        }

        public void SaveReceipts(List<Receipt> receipts, HttpClient http)
        {
            throw new NotImplementedException();
        }

        public void UpdateReceipts(List<Receipt> receipts, HttpClient http)
        {
            throw new NotImplementedException();
        }
    }
}
