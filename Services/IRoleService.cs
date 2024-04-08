using WizardFormBackend.DTOs;
using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> AddRoleAsync(RoleDTO roleDTO);
        Task DeleteRoleAsync(int roleId);
        Task<string> GetRoleTypeAsync(int roleId);
        Task<IEnumerable<RoleDTO>> GetRolesAsync();
    }
}