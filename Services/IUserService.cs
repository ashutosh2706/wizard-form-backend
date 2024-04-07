using WizardFormBackend.DTOs;
using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDTO userDTO);
        Task AllowUser(long userId, bool allowed);
        Task DeleteUserAsync(long userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserResponseDTO>> GetUsersAsync();
        Task UpdateUserAsync(UserDTO userDTO);
    }
}