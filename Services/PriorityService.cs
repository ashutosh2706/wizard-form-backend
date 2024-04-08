using WizardFormBackend.Repositories;
using WizardFormBackend.Models;
using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public class PriorityService(IPriorityRepository priorityRepository) : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository = priorityRepository;

        public async Task<IEnumerable<PriorityDTO>> GetPrioritiesAsync()
        {
            IEnumerable<Priority> priorities = await _priorityRepository.GetAllPriorityAsync();
            List<PriorityDTO> priorityDTOs = new List<PriorityDTO>();
            foreach (var priority in priorities)
            {
                priorityDTOs.Add(new PriorityDTO
                {
                    PriorityCode = priority.PriorityCode,
                    Description = priority.Description
                });
            }

            return priorityDTOs;
        }

        public async Task<PriorityDTO> AddPriorityAsync(PriorityDTO priorityDTO)
        {
            Priority priority = new()
            {
                PriorityCode = priorityDTO.PriorityCode,
                Description = priorityDTO.Description
            };

            Priority newPriority = await _priorityRepository.AddPriorityAsync(priority);
            return new PriorityDTO { PriorityCode = newPriority.PriorityCode, Description = newPriority.Description };
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
