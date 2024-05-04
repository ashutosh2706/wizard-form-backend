using WizardFormBackend.Repositories;
using WizardFormBackend.Models;
using WizardFormBackend.Dto;
using AutoMapper;

namespace WizardFormBackend.Services
{
    public class PriorityService(IPriorityRepository priorityRepository, IMapper mapper) : IPriorityService
    {
        private readonly IPriorityRepository _priorityRepository = priorityRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<PriorityDto>> GetPrioritiesAsync()
        {
            IEnumerable<Priority> priorities = await _priorityRepository.GetAllPriorityAsync();
            return _mapper.Map<IEnumerable<Priority>, List<PriorityDto>>(priorities);
        }

        public async Task<PriorityDto> AddPriorityAsync(PriorityDto priorityDto)
        {

            Priority priority = _mapper.Map<PriorityDto, Priority>(priorityDto);
            Priority newPriority = await _priorityRepository.AddPriorityAsync(priority);
            return new PriorityDto { PriorityCode = newPriority.PriorityCode, Description = newPriority.Description };
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
