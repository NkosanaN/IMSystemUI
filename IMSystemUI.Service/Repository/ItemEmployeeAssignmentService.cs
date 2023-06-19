using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ItemEmployeeAssignmentService : IItemEmployeeAssignmentService
{
    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";

    public ItemEmployeeAssignmentService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<ItemEmployeeAssignment>> GetAllItemEmployeeAssignmentsAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{apiUrl}/ItemEmployeeAssignment");

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

            var httpResponse = await _client.GetAsync($"{apiUrl}/ItemEmployeeAssignment/{id}");

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

            var httpResponse = await _client.PostAsync($"{apiUrl}/ItemEmployeeAssignment",
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
        var httpResponse = await _client.DeleteAsync($"{apiUrl}/ItemEmployeeAssignment/{id}");

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

            var httpResponse = await _client.PutAsync($"{apiUrl}/ItemEmployeeAssignment/{iItem.AssigmentId}/ReturnItem",
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