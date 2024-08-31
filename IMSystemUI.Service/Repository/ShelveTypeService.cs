using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ShelveTypeService : IShelveTypeService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    public ShelveTypeService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }
    public async Task<IEnumerable<ShelveType>> GetAllShelveTypesAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/ShelveType");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var shelveType = JsonConvert.DeserializeObject<IEnumerable<ShelveType>>(content);

            return shelveType!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ShelveType> GetAllShelveTypeAsync(Guid id, string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl").Value}/ShelveType/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var shelveType = JsonConvert.DeserializeObject<ShelveType>(content);

            return shelveType!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ShelveType> CreateShelveTypeAsync(ShelveType shelvetype, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(shelvetype);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl").Value}/ShelveType", new StringContent(content, Encoding.Default, "application/json"));

            if (httpResponse.ReasonPhrase.Contains("Bad Request"))
            {
                throw new Exception("ShelveType already exist");
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the ShelveType");
            }
     
            var createdShelveType =
                JsonConvert.DeserializeObject<ShelveType>(await httpResponse.Content.ReadAsStringAsync());

            return createdShelveType!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveShelveTypeAsync(Guid id, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var httpResponse = await _client.DeleteAsync($"{_config.GetSection("apiUrl").Value}/ShelveType/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the ShelveType");
        }
    }

    public async Task UpdateShelveTypeAsync(Guid id, ShelveType shelvetype, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(shelvetype);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PutAsync($"{_config.GetSection("apiUrl").Value}/ShelveType/{id}",
                new StringContent(content, Encoding.Default, "application/json"));

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

