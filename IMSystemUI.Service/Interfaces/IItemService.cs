using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<Item> GetAllItemAsync(Guid id);
    Task<Item> CreateItemAsync(Item item);
    Task RemoveItemAsync(Guid id);
}

