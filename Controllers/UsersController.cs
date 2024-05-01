using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Services;
using WizardFormBackend.Utils;

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
        public async Task<IActionResult> GetUsers([FromQuery]QueryParams queryParams)
        {
            PaginatedResponseDTO<UserResponseDto> response = await _userService.GetUsersAsync(queryParams.SearchTerm, queryParams.PageNumber, queryParams.PageSize);
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(LoginDTO loginDTO)
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

        [HttpPut("change-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole(ChangeRoleDTO changeRoleDTO)
        {
            await _userService.ChangeRoleAsync(changeRoleDTO.UserId, changeRoleDTO.RoleId);
            return Ok();
        }

        [HttpDelete("delete/{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(long UserId)
        {
            await _userService.DeleteUserAsync(UserId);
            return NoContent();
        }

    }
}
