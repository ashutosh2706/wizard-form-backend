using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Repositories
{
    public interface IStatusRepository
    {
        Task<Status> AddStatusAsync(Status status);
        Task DeleteStatusAsync(Status status);
        Task<IEnumerable<Status>> GetAllStatusAsync();
        Task<Status?> GetStatusByStatusCodeAsync(int statusCode);
        Task UpdateStatusAsync(Status status);
    }
}