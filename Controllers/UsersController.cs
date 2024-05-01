using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.DTOs;
using WizardFormBackend.DTOs.Paginated;
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
        public async Task<IActionResult> GetUsers(string query = "", int page = 1, int limit = 10)
        {
            PaginatedUserResponseDTO response = await _userService.GetUsersAsync(query, page, limit);
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            string? token = await _userService.AuthenticateUserAsync(loginDTO);
            return token != null && token != "" ? Ok(token) : token != null && token == "" ? Unauthorized() : BadRequest();
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
