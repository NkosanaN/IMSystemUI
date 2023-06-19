using IMSystemUI.Domain;
using IMSystemUI.Service.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace IMSystemUI.Service.Repository;

public class UserService : IUserService
{
    private readonly HttpClient _client;
    private const string apiUrl = "http://localhost:5293/api";
    public UserService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(string token)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var httpResponse = await _client.GetAsync($"{apiUrl}/User");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<IEnumerable<User>>(content);

            return user!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<UserDto> RegisterUser(Register reg)
    {
        try
        {
            var content = JsonConvert.SerializeObject(reg);

            var httpResponse = await _client.PostAsync($"{apiUrl}/Account/Register", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot create new User");
            }

            var createdUser =
                JsonConvert.DeserializeObject<UserDto>(await httpResponse.Content.ReadAsStringAsync());
            return createdUser!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<User> GetAllUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    //public async Task<Login> Login(Login login)
    //{
    //    try
    //    {
    //        var content = JsonConvert.SerializeObject(login);
    //        var httpResponse = await _client.PostAsync($"{apiUrl}/Account/login",
    //            new StringContent(content, Encoding.Default, "application/json"));

    //        if (!httpResponse.IsSuccessStatusCode)
    //        {
    //            throw new Exception("Cannot create the Supplier");
    //        }

    //        var createdSupplier =
    //            JsonConvert.DeserializeObject<Login>(await httpResponse.Content.ReadAsStringAsync());

    //        return createdSupplier!;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        throw;
    //    }
    //}
    public async Task LogoutAsync()
    {
        try
        {
            var httpResponse = await _client.GetAsync($"{apiUrl}/Account/Logout");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot Logout ");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<IEnumerable<User>>(content);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task RemoveUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}