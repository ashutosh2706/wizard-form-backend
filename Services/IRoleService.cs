using WizardFormBackend.Dto;

namespace WizardFormBackend.Services
{
    public interface IRoleService
    {
        Task<RoleDto> AddRoleAsync(RoleDto roleDto);
        Task DeleteRoleAsync(int roleId);
        Task<string> GetRoleTypeAsync(int roleId);
        Task<IEnumerable<RoleDto>> GetRolesAsync();
    }
}