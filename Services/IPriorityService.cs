using WizardFormBackend.DTOs;
using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IPriorityService
    {
        Task<Priority> AddPriorityAsync(PriorityDTO priorityDTO);
        Task DeletePriorityAsync(int priorityCode);
        Task<IEnumerable<Priority>> GetPrioritiesAsync();
    }
}