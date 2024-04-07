using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data;
using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WizardFormDbContext _dbContext;
        public RoleRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByRoleIdAsync(int roleId)
        {
            return await _dbContext.Roles.FindAsync(roleId);
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Role role)
        {
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}
