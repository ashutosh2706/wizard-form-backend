using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<IEnumerable<User>> GetAllUserAsync(string searchKeyword);
        Task<User?> GetUserByUserIdAsync(long userId);
        Task UpdateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string Email);
    }
}