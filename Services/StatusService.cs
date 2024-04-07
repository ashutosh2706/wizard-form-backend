using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _statusRepository.GetAllStatusAsync();
        }

        public async Task<Status> AddStatusAsync(StatusDTO statusDTO)
        {
            Status status = new()
            {
                Description = statusDTO.Description,
            };

            return await _statusRepository.AddStatusAsync(status);
        }

        public async Task DeleteStatusAsync(int statusCode)
        {
            Status? existingStatus = await _statusRepository.GetStatusByStatusCodeAsync(statusCode);
            if (existingStatus != null)
            {
                await _statusRepository.DeleteStatusAsync(existingStatus);
            }
        }
    }
}
