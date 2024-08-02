using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string userName);
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        
    }
}
