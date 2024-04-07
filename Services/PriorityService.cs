using WizardFormBackend.Repositories;
using WizardFormBackend.Models;
using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public class PriorityService(IPriorityRepository priorityRepository) : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository = priorityRepository;

        public async Task<IEnumerable<Priority>> GetPrioritiesAsync()
        {
            return await _priorityRepository.GetAllPriorityAsync();
        }

        public async Task<Priority> AddPriorityAsync(PriorityDTO priorityDTO)
        {
            Priority priority = new()
            {
                Description = priorityDTO.Description
            };

            return await _priorityRepository.AddPriorityAsync(priority);
        }

        public async Task DeletePriorityAsync(int priorityCode)
        {
            Priority? existingPriority = await _priorityRepository.GetPriorityByPriorityCodeAsync(priorityCode);
            if (existingPriority != null)
            {
                await _priorityRepository.DeletePriorityAsync(existingPriority);
            }
        }
    }
}
