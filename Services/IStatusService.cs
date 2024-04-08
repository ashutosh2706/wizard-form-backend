using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public interface IStatusService
    {
        Task<StatusDTO> AddStatusAsync(StatusDTO statusDTO);
        Task DeleteStatusAsync(int statusCode);
        Task<IEnumerable<StatusDTO>> GetStatusesAsync();
    }
}