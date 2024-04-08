using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public interface IPriorityService
    {
        Task<PriorityDTO> AddPriorityAsync(PriorityDTO priorityDTO);
        Task DeletePriorityAsync(int priorityCode);
        Task<IEnumerable<PriorityDTO>> GetPrioritiesAsync();
    }
}