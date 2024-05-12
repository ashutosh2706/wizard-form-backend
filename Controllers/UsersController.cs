using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Data.Dto;
using WizardFormBackend.Data.Models;
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
            PagedResponseDto<UserResponseDto> response = await _userService.GetUsersAsync(queryParams.SearchTerm, queryParams.PageNumber, queryParams.PageSize);
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromForm]LoginDto loginDto)
        {
            string? token = await _userService.AuthenticateUserAsync(loginDto);
            return token != null && token != "" ? Ok(token) : token != null && token == "" ? Unauthorized() : BadRequest();
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm]UserDto userDto)
        {
            User newUser = await _userService.AddUserAsync(userDto);
            return Created("/Users", newUser);
        }


        [HttpPut("allow/{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AllowUser(long UserId)
        {
            bool actionPerformed = await _userService.AllowUserAsync(UserId);
            return actionPerformed ? Ok() : NotFound();
        }


        [HttpPut("change-role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeRole(ChangeRoleDto changeRoleDto)
        {
            await _userService.ChangeRoleAsync(changeRoleDto.UserId, changeRoleDto.RoleId);
            return Ok();
        }


        [HttpDelete("delete/{UserId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(long UserId)
        {
            bool actionPerformed = await _userService.DeleteUserAsync(UserId);
            return actionPerformed ? NoContent() : NotFound();
        }

    }
}
