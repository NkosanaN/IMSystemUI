using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace IMSystemUI.Service.Repository;

public class DepartmentService : IDepartmentService
{
    private readonly string bearerToken = Constant.token;


    private readonly HttpClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private const string apiUrl = "http://localhost:5293/api";

    public DepartmentService(HttpClient client , IHttpContextAccessor httpContextAccessor)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        try
        {

           var t = _httpContextAccessor.HttpContext.User.Identity.Name;
           //var t = _httpContextAccessor.HttpContext.User;
           //var t = _httpContextAccessor.HttpContext.User.Identity.Name;

            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Department");

            if (!httpResponse.IsSuccessStatusCode)
            {
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

    public async Task<Department> GetAllDepartmentAsync(Guid id)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Bearer", bearerToken);

            var httpResponse = await _client.GetAsync($"{apiUrl}/Department");

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

    public async Task<Department> CreateDepartmentAsync(Department department)
    {
        try
        {
            var content = JsonConvert.SerializeObject(department);

            var httpResponse = await _client.PostAsync($"{apiUrl}/Department",
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

    public async Task RemoveDepartmentAsync(Guid id)
    {
        var httpResponse = await _client.DeleteAsync($"{apiUrl}/Department/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception("Cannot delete the Department");
        }
    }
}

