using WizardFormBackend.DTOs;
using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDTO userDTO);
        Task AllowUserAsync(long userId);
        Task DeleteUserAsync(long userId);
        Task<IEnumerable<UserResponseDTO>> GetUsersAsync();
        Task UpdateUserAsync(UserDTO userDTO);
        Task<string?> AuthenticateUserAsync(LoginDTO loginDTO);
        Task ChangeRoleAsync(long userId, int roleId);
    }
}