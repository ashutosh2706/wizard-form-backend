using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data.Context;
using WizardFormBackend.Data.Models;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WizardFormDbContext _dbContext;

        public UserRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(string searchKeyword)
        {
            searchKeyword = Util.Sanitize(searchKeyword.ToLower());

            return await _dbContext.Users.Where(r => r.UserId.ToString() == searchKeyword ||
                r.FirstName.ToLower().Contains(searchKeyword) ||
                r.LastName.ToLower().Contains(searchKeyword) ||
                r.Email.ToLower().Contains(searchKeyword) ||
                r.RoleId == (int)Constants.Roles.User && "user".Contains(searchKeyword) ||
                r.RoleId == (int)Constants.Roles.Admin && "admin".Contains(searchKeyword)
            ).ToListAsync();
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
