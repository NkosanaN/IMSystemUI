using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ItemService : IItemService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public ItemService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }
    public async Task<IEnumerable<Item>> GetAllItemsAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Item");

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
    public async Task<Item> GetAllItemAsync(Guid id, string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/Item/{id}");

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

    public async Task<Item> CreateItemAsync(Item item, string token)
    {
        try
        {
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlNhbTFAZ21haWwuY29tMyIsIm5hbWVpZCI6IjRmNWRjZjU3LTIzMmMtNDBmZS05MWRkLTczNWFmZTU1MjgzNSIsImVtYWlsIjoiM1NhbTFAZ21haWwuY29tIiwibmJmIjoxNzI1MTEwNTYzLCJleHAiOjE3MjU3MTUzNjMsImlhdCI6MTcyNTExMDU2M30.VYZPpPlVYKdf2117YJHfbPapAZ_0-MgWyJmQAEAji2A";

            var content = JsonConvert.SerializeObject(item);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/Item", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create new the Item");
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

    public async Task RemoveItemAsync(Guid id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var httpResponse = await _client.DeleteAsync($"{_config.GetSection("apiUrl").Value}/Item/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the Item");
        }
    }

    public async Task<bool> BookRepair(Item item, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(item);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PutAsync($"{_config.GetSection("apiUrl").Value}/Item/{item.ItemId}/BookRepairItem",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot update the Item");
            }
            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task UpdateItemAsync(Guid id, Item item, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(item);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PutAsync($"{_config.GetSection("apiUrl").Value}/Item/{id}", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot update the ShelveType");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

