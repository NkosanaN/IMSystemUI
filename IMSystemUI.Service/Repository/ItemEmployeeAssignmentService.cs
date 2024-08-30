using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ItemEmployeeAssignmentService : IItemEmployeeAssignmentService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public ItemEmployeeAssignmentService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }

    public async Task<IEnumerable<ItemEmployeeAssignment>> GetAllItemEmployeeAssignmentsAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/ItemEmployeeAssignment");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var itemsTransfer = JsonConvert.DeserializeObject<IEnumerable<ItemEmployeeAssignment>>(content);

            return itemsTransfer!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ItemEmployeeAssignment> GetAllItemEmployeeAssignmentAsync(Guid id, string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/ItemEmployeeAssignment/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var itemsTransfer = JsonConvert.DeserializeObject<ItemEmployeeAssignment>(content);

            return itemsTransfer!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ItemEmployeeAssignment> CreateItemEmployeeAssignmentAsync(ItemEmployeeAssignment itemTransfer,
        string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(itemTransfer);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/ItemEmployeeAssignment",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the ItemEmployeeAssignment");
            }

            var createdItemTransfer =
                JsonConvert.DeserializeObject<ItemEmployeeAssignment>(await httpResponse.Content.ReadAsStringAsync());
            return createdItemTransfer!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveItemEmployeeAssignmentAsync(Guid id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpResponse = await _client.DeleteAsync($"{_config.GetSection("apiUrl").Value}/ItemEmployeeAssignment/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the ItemEmployeeAssignment");
        }
    }

    public async Task<bool> ReturnItem(ItemEmployeeAssignment iItem, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(iItem);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PutAsync($"{_config.GetSection("apiUrl").Value}/ItemEmployeeAssignment/{iItem.AssigmentId}/ReturnItem",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot Book for Return Item.");
            }

            return true;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}