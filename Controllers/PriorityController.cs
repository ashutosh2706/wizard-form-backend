using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.DTOs;
using WizardFormBackend.Services;

namespace WizardFormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            IEnumerable<PriorityDTO> priorityDTOs = await _priorityService.GetPrioritiesAsync();
            return Ok(priorityDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> AddPriority(PriorityDTO priorityDTO)
        {
            PriorityDTO responseDTO = await _priorityService.AddPriorityAsync(priorityDTO);
            return Created("/Priority", responseDTO);
        }

        [HttpDelete("{PriorityCode}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePriority(int PriorityCode)
        {
            await _priorityService.DeletePriorityAsync(PriorityCode);
            return NoContent();
        }
    }
}
