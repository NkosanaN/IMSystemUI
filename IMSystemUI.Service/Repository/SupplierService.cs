using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class SupplierService : ISupplierService
{
    private readonly IHttpClientExtensions _client;
    //readonly string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QxMjNAZ21haWwuY29tIiwibmFtZWlkIjoiZTkxZWJiYWMtMmY5NC00NTY5LTg1YzktZDVhZDk2YmYxODExIiwiZW1haWwiOiJ0ZXN0MTIzQGdtYWlsLmNvbSIsIm5iZiI6MTY4MzU0Mzc5NCwiZXhwIjoxNjg0MTQ4NTk0LCJpYXQiOjE2ODM1NDM3OTR9.MYx48wDubgIrgJkhxd4fQqjtnhtRVSBApF6K3EIESO4";
    //private const string apiUrl = "http://localhost:5293/api";
    public SupplierService(IHttpClientExtensions httpClientExtensions)
    {
        _client = httpClientExtensions ?? throw new ArgumentNullException(nameof(httpClientExtensions));
    }
    public async Task<IEnumerable<Supplier>> GetAllSupplierAsync()
    {
        try
        {
            var response = await _client.GetAllAsync<IEnumerable<Supplier>>();
            
            return (IEnumerable<Supplier>)response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Supplier> GetSupplierAsync()
    {
        //try
        //{
        //    var response = await _client.GetApiDataAsync($"{apiUrl}/Supplier", bearerToken);
        //    var resp = await response.Content.ReadAsStringAsync();

        //    var supplier = JsonConvert.DeserializeObject<Supplier>(resp);
        //    return supplier!;
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}
        throw new NotImplementedException();
    }

    public Task<Supplier> RemoveSupplierAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreateSupplierAsync(string supplier)
    {
        //try
        //{
        //    var response = await _client.CreateApiDataAsync($"{apiUrl}/Supplier", bearerToken , supplier);

        //    return string.Empty;

        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}
        throw new NotImplementedException();
    }

    public Task CreateSupplierAsync(Supplier supplier)
    {
        throw new NotImplementedException();
    }
}
