using Shared.Models;

namespace Shared.Service;

public interface IItemService
{
    void SaveBestBeforeDate(Item item);
}

public class ItemService : IItemService
{
    public void SaveBestBeforeDate(Item item)
    {
        //Put logic here to save
        if (item.BestBeforeDate < DateTime.Now)
        {
            throw new Exception("Datumet kan inte vara i det förflutna.");
        }

        Console.WriteLine($"Bäst före-datum för {item.Name} sparat: {item.BestBeforeDate}");
    }
}