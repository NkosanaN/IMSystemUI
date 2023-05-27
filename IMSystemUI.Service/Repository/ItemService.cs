using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ItemService : IItemService
{
    readonly string bearerToken = Constant.token;

    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";

    public ItemService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Item");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<IEnumerable<Item>>(content);

            return items!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Item> GetAllItemAsync(Guid id)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Items/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<Item>(content);

            return items!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        try
        {
            var content = JsonConvert.SerializeObject(item);

            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await _client.PostAsync($"{apiUrl}/Item",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the Supplier");
            }

            var createdItem =
                JsonConvert.DeserializeObject<Item>(await httpResponse.Content.ReadAsStringAsync());
            return createdItem!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveItemAsync(Guid id)
    {
        var httpResponse = await _client.DeleteAsync($"{apiUrl}/Item/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the Item");
        }
    }
}
