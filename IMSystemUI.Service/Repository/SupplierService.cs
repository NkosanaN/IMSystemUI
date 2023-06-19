using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class SupplierService : ISupplierService
{
    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";

    public SupplierService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<Supplier>> GetAllSupplierAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Supplier");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var suppliers = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(content);

            return suppliers!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Supplier> GetSupplierAsync(Guid id, string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Supplier/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var supplier = JsonConvert.DeserializeObject<Supplier>(content);

            return supplier!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveSupplierAsync(Guid id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var httpResponse = await _client.DeleteAsync($"{apiUrl}/Supplier/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the Supplier ");
        }
    }

    public async Task<Supplier> CreateSupplierAsync(Supplier supplier, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(supplier);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PostAsync($"{apiUrl}/Supplier", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the Supplier");
            }

            var createdSupplier =
                JsonConvert.DeserializeObject<Supplier>(await httpResponse.Content.ReadAsStringAsync());
            return createdSupplier!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateSupplierAsync(Guid id, Supplier supplier, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(supplier);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PutAsync($"{apiUrl}/Supplier/{id}",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot update the Supplier");
            }

            //var createdSupplier =
            //    JsonConvert.DeserializeObject<Supplier>(await httpResponse.Content.ReadAsStringAsync());
            //return createdSupplier!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
