using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Services;

namespace WizardFormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<UserResponseDTO> responseDTOs = await _userService.GetUsersAsync();
            return Ok(responseDTOs);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            string? token = await _userService.AuthenticateUserAsync(loginDTO);
            return token != null ? Ok(token) : BadRequest();
            
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {
            User newUser = await _userService.AddUserAsync(userDTO);
            return Created("/Users", newUser);
        }

        [HttpPut("allow/{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllowUser(long UserId)
        {
            await _userService.AllowUserAsync(UserId);
            return Ok();
        }

        [HttpPut("roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole(ChangeRoleDTO changeRoleDTO)
        {
            await _userService.ChangeRoleAsync(changeRoleDTO.UserId, changeRoleDTO.RoleId);
            return Ok();
        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(long UserId)
        {
            await _userService.DeleteUserAsync(UserId);
            return NoContent();
        }

    }
}
