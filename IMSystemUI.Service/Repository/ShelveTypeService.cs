﻿using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class ShelveTypeService : IShelveTypeService
{
    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";

    public ShelveTypeService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<IEnumerable<ShelveType>> GetAllShelveTypesAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{apiUrl}/ShelveType");

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

            var httpResponse = await _client.GetAsync($"{apiUrl}/ShelveType/{id}");

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

            var httpResponse = await _client.PostAsync($"{apiUrl}/ShelveType", new StringContent(content, Encoding.Default, "application/json"));

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

        var httpResponse = await _client.DeleteAsync($"{apiUrl}/ShelveType/{id}");

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

            var httpResponse = await _client.PutAsync($"{apiUrl}/ShelveTypes/{id}",
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

