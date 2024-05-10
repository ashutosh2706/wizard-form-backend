using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Data.Dto;
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
            IEnumerable<PriorityDto> priorityDTOs = await _priorityService.GetPrioritiesAsync();
            return Ok(priorityDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddPriority(PriorityDto priorityDto)
        {
            PriorityDto responseDTO = await _priorityService.AddPriorityAsync(priorityDto);
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
