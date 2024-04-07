using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> AddRoleAsync(Role role);
        Task DeleteRoleAsync(Role role);
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role?> GetRoleByRoleIdAsync(int roleId);
        Task UpdateRoleAsync(Role role);
    }
}