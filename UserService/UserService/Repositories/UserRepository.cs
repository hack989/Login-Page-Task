using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddUserAsync(User user)
        {
            await this._dbContext.AddAsync(user);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await this._dbContext.Users.SingleOrDefaultAsync(x => x.username == userName);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await this._dbContext.Users.SingleOrDefaultAsync(x => x.email == email);
        }

       
    }
}
