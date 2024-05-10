using WizardFormBackend.Data.Dto;

namespace WizardFormBackend.Services
{
    public interface IPriorityService
    {
        Task<PriorityDto> AddPriorityAsync(PriorityDto priorityDto);
        Task DeletePriorityAsync(int priorityCode);
        Task<IEnumerable<PriorityDto>> GetPrioritiesAsync();
    }
}