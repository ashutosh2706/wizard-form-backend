using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data;
using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WizardFormDbContext _dbContext;

        public UserRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetUserByUserIdAsync(long userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            return await _dbContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
        }
    }
}
