using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Helpers;

public  class HttpClientExtensions : IHttpClientExtensions
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    readonly string bearerToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InRlc3QxMjNAZ21haWwuY29tIiwibmFtZWlkIjoiZTkxZWJiYWMtMmY5NC00NTY5LTg1YzktZDVhZDk2YmYxODExIiwiZW1haWwiOiJ0ZXN0MTIzQGdtYWlsLmNvbSIsIm5iZiI6MTY4MzU0Mzc5NCwiZXhwIjoxNjg0MTQ4NTk0LCJpYXQiOjE2ODM1NDM3OTR9.MYx48wDubgIrgJkhxd4fQqjtnhtRVSBApF6K3EIESO4";
    private readonly string _baseURL = "http://localhost:5293/api";
    public HttpClientExtensions(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
       
        _httpContextAccessor = httpContextAccessor;
    }

    private string GetBearerToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            return authHeader.Substring("Bearer ".Length);
        }

        //return null;
        return bearerToken;

    }
    public async Task<IEnumerable<T>> GetAllAsync<T>()
    {
        var entity = typeof(T);
        var controller = entity.Name;

        var request = new HttpRequestMessage(HttpMethod.Get, typeof(T).Name.ToLower());
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetBearerToken());
        
        _httpClient.BaseAddress = new Uri($"{_baseURL}/{controller}");

        if(_httpClient.BaseAddress == null)
            _httpClient.BaseAddress = new Uri($"{_baseURL}/{controller}");

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IEnumerable<T>>(content);
    }

    public async Task CreateAsync<T>(T entity)
    {
        var content = JsonConvert.SerializeObject(entity);
        var request = new HttpRequestMessage(HttpMethod.Post, typeof(T).Name.ToLower());
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetBearerToken());

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task<T> GetByIdAsync<T>(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{typeof(T).Name.ToLower()}/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetBearerToken());

        var entity = typeof(T);
        var controller = entity.Name;

        _httpClient.BaseAddress = new Uri($"{_baseURL}/{controller}");

        if (_httpClient.BaseAddress == null)
            _httpClient.BaseAddress = new Uri($"{_baseURL}/{controller}");

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return default(T);
        }
        else
        {
            throw new Exception($"Failed to retrieve {typeof(T).Name.ToLower()} with ID {id}: {response.StatusCode}");
        }
    }

    public Task UpdateAsync<T>(Guid id, T entity)
    {
        throw new NotImplementedException();
    }

    //public async Task<HttpResponseMessage> GetApiDataAsync(string apiUrl, string bearerToken)
    //{
    //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

    //    var response = await _httpClient.GetAsync(apiUrl);
    //    return response;
    //}


    //public async Task<string> CreateApiDataAsync(string apiUrl, string bearerToken, string newData)
    //{
    //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

    //    using (var content = new StringContent(newData, Encoding.UTF8, "application/json"))
    //    {
    //        using (var response = await _httpClient.PostAsync(apiUrl, content))
    //        {
    //            if (response.IsSuccessStatusCode)
    //            {
    //                var result = await response.Content.ReadAsStringAsync();
    //                return result;
    //            }
    //            else
    //            {
    //                throw new Exception($"API Error: {response.ReasonPhrase}");
    //            }
    //        }
    //    }
    //}

    //public Task<string> UpdateApiDataAsync(string apiUrl, string bearerToken, string updatedData)
    //{
    //    throw new NotImplementedException();
    //}
}
