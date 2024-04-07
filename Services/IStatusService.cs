using WizardFormBackend.DTOs;
using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IStatusService
    {
        Task<Status> AddStatusAsync(StatusDTO statusDTO);
        Task DeleteStatusAsync(int statusCode);
        Task<IEnumerable<Status>> GetStatusesAsync();
    }
}