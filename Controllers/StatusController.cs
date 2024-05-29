using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Services;
using Microsoft.AspNetCore.Authorization;
using WizardFormBackend.Data.Dto;

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
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AddStatus(StatusDto statusDto)
        {
            StatusDto response = await _statusService.AddStatusAsync(statusDto);
            return Created("/Status", response);
        }

        [HttpDelete("{StatusCode}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteStatusAsync(int StatusCode)
        {
            await _statusService.DeleteStatusAsync(StatusCode);
            return NoContent();
        }
    }
}
