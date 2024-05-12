using WizardFormBackend.Data.Dto;
using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserDto userDto);
        Task<bool> AllowUserAsync(long userId);
        Task<bool> DeleteUserAsync(long userId);
        Task<PagedResponseDto<UserResponseDto>> GetUsersAsync(string searchTerm, int pageNumber, int pageSize);
        Task UpdateUserAsync(UserDto userDto);
        Task<string?> AuthenticateUserAsync(LoginDto loginDto);
        Task ChangeRoleAsync(long userId, int roleId);
    }
}