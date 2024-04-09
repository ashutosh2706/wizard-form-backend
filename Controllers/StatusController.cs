using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Services;
using WizardFormBackend.DTOs;
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
            IEnumerable<StatusDTO> statusDTOs = await _statusService.GetStatusesAsync();
            return Ok(statusDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddStatus(StatusDTO statusDTO)
        {
            StatusDTO response = await _statusService.AddStatusAsync(statusDTO);
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
