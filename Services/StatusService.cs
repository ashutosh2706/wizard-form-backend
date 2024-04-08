using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;

        public async Task<IEnumerable<StatusDTO>> GetStatusesAsync()
        {
            IEnumerable<Status> statuses = await _statusRepository.GetAllStatusAsync();
            List<StatusDTO> statusDTOs = new List<StatusDTO>();
            foreach (Status status in statuses)
            {
                statusDTOs.Add(new StatusDTO
                {
                    StatusCode = status.StatusCode,
                    Description = status.Description
                });
            }
            return statusDTOs;
        }

        public async Task<StatusDTO> AddStatusAsync(StatusDTO statusDTO)
        {
            Status status = new()
            {
                StatusCode = statusDTO.StatusCode,
                Description = statusDTO.Description,
            };

            Status newStatus = await _statusRepository.AddStatusAsync(status);
            return new StatusDTO { StatusCode = newStatus.StatusCode, Description = newStatus.Description };
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
