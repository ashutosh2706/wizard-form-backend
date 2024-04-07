using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<IEnumerable<RoleDTO>> GetRolesAsync()
        {
            IEnumerable<Role> roles = await _roleRepository.GetAllRoleAsync();
            List<RoleDTO> roleDTOs = new List<RoleDTO>();
            foreach (Role role in roles)
            {
                roleDTOs.Add(new RoleDTO { RoleId = role.RoleId, RoleType = role.RoleType });
            }
            return roleDTOs;
        }

        public async Task<RoleDTO> AddRoleAsync(RoleDTO roleDTO)
        {
            Role role = new()
            {
                RoleId = roleDTO.RoleId,
                RoleType = roleDTO.RoleType
            };

            Role newRole = await _roleRepository.AddRoleAsync(role);
            return new RoleDTO { RoleId = newRole.RoleId, RoleType = newRole.RoleType };
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            Role? existingRole = await _roleRepository.GetRoleByRoleIdAsync(roleId);
            if (existingRole != null)
            {
                await _roleRepository.DeleteRoleAsync(existingRole);
            }
        }

    }
}
