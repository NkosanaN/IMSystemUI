using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class DepartmentService : IDepartmentService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;

    public DepartmentService(HttpClient client, IConfiguration config)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _config = config ?? throw new ArgumentNullException("config");
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl")}/Department");

            if (!httpResponse.IsSuccessStatusCode)
            {
                //Q : how to make squiggly line go away?
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var departments = JsonConvert.DeserializeObject<IEnumerable<Department>>(content);

            return departments!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Department> GetAllDepartmentAsync(Guid id, string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{_config.GetSection("apiUrl")}/Department");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var department = JsonConvert.DeserializeObject<Department>(content);

            return department!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Department> CreateDepartmentAsync(Department department, string token)
    {
        try
        {
            var content = JsonConvert.SerializeObject(department);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.PostAsync($"{_config.GetSection("apiUrl")}/Department",
                new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create the Department");
            }

            var createdDepartment =
                JsonConvert.DeserializeObject<Department>(await httpResponse.Content.ReadAsStringAsync());

            return createdDepartment!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveDepartmentAsync(Guid id , string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpResponse = await _client.DeleteAsync($"{_config.GetSection("apiUrl")}/Department/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the Department");
        }
    }
}

