using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Services;
using WizardFormBackend.Dto;
using Microsoft.AspNetCore.Authorization;

namespace WizardFormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            IEnumerable<StatusDto> statusDTOs = await _statusService.GetStatusesAsync();
            return Ok(statusDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddStatus(StatusDto statusDto)
        {
            StatusDto response = await _statusService.AddStatusAsync(statusDto);
            return Created("/Status", response);
        }

        [HttpDelete("{StatusCode}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStatusAsync(int StatusCode)
        {
            await _statusService.DeleteStatusAsync(StatusCode);
            return NoContent();
        }
    }
}
