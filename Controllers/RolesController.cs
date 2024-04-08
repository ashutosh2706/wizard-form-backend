using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO)
        {
            RoleDTO role = await _roleService.AddRoleAsync(roleDTO);
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
