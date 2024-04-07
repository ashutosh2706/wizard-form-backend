using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public interface IPriorityRepository
    {
        Task<Priority> AddPriorityAsync(Priority priority);
        Task DeletePriorityAsync(Priority priority);
        Task<IEnumerable<Priority>> GetAllPriorityAsync();
        Task<Priority?> GetPriorityByPriorityCodeAsync(int priorityCode);
        Task UpdatePriorityAsync(Priority priority);
    }
}