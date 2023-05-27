using IMSystemUI.Domain;

namespace IMSystemUI.Service.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetAllUserAsync(Guid id);
   // Task Login(Login login);
    Task<User> CreateUserAsync(User user);
    Task RemoveUserAsync(Guid id);
    Task<UserDto> RegisterUser(Register reg);
}
