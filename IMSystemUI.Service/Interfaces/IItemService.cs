using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItemsAsync(string token);
    Task<Item> GetAllItemAsync(Guid id, string token);
    Task<Item> CreateItemAsync(Item item, string token);
    Task<bool> BookRepair(Item item, string token);
    Task RemoveItemAsync(Guid id, string token);
    Task UpdateItemAsync(Guid id, Item item, string token);
}

