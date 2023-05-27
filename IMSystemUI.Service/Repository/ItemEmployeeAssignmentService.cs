using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ItemEmployeeAssignmentService : IItemEmployeeAssignmentService
{
    readonly string bearerToken = Constant.token;

    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";
    public ItemEmployeeAssignmentService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<IEnumerable<ItemEmployeeAssignment>> GetAllItemEmployeeAssignmentsAsync()
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

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

    public async Task<ItemEmployeeAssignment> GetAllItemEmployeeAssignmentAsync(Guid id)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

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

    public async Task<ItemEmployeeAssignment> CreateItemEmployeeAssignmentAsync(ItemEmployeeAssignment itemTransfer)
    {
        try
        {
            var content = JsonConvert.SerializeObject(itemTransfer);
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

    public async Task RemoveItemEmployeeAssignmentAsync(Guid id)
    {
        var httpResponse = await _client.DeleteAsync($"{apiUrl}/ItemEmployeeAssignment/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the ItemEmployeeAssignment");
        }
    }
}