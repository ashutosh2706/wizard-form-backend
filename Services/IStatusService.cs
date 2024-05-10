using WizardFormBackend.Data.Dto;

namespace WizardFormBackend.Services
{
    public interface IStatusService
    {
        Task<StatusDto> AddStatusAsync(StatusDto statusDto);
        Task DeleteStatusAsync(int statusCode);
        Task<IEnumerable<StatusDto>> GetStatusesAsync();
    }
}