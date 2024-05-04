using AutoMapper;
using WizardFormBackend.Dto;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class RoleService(IRoleRepository roleRepository, IMapper mapper) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            IEnumerable<Role> roles = await _roleRepository.GetAllRoleAsync();
            List<RoleDto> roleDTOs = _mapper.Map<IEnumerable<Role>, List<RoleDto>>(roles);
            return roleDTOs;
        }

        public async Task<RoleDto> AddRoleAsync(RoleDto roleDto)
        {
            Role role = _mapper.Map<RoleDto, Role>(roleDto);
            Role newRole = await _roleRepository.AddRoleAsync(role);
            return _mapper.Map<Role, RoleDto>(newRole);
        }

        public async Task<string> GetRoleTypeAsync(int roleId)
        {
            Role? role = await _roleRepository.GetRoleByRoleIdAsync(roleId);
            return role != null ? role.RoleType : string.Empty;
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
