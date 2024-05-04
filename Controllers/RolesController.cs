using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Dto;
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
            IEnumerable<RoleDto> roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRole(RoleDto roleDto)
        {
            RoleDto role = await _roleService.AddRoleAsync(roleDto);
            return Created("/Roles", role);
        }

        [HttpDelete("{RoleId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRole(int RoleId)
        {
            await _roleService.DeleteRoleAsync(RoleId);
            return NoContent();
        }
    }
}
