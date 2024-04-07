using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.DTOs;
using WizardFormBackend.Services;

namespace WizardFormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService) 
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            IEnumerable<RoleDTO> roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            RoleDTO role = await _roleService.AddRoleAsync(roleDTO);
            return Created("/Roles", role);
        }

        [HttpDelete("roleId")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            await _roleService.DeleteRoleAsync(roleId);
            return Ok();
        }
    }
}
