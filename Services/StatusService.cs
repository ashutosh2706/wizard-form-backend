using AutoMapper;
using WizardFormBackend.Dto;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class StatusService(IStatusRepository statusRepository, IMapper mapper) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<StatusDto>> GetStatusesAsync()
        {
            IEnumerable<Status> statuses = await _statusRepository.GetAllStatusAsync();
            List<StatusDto> statusDTOs = _mapper.Map<IEnumerable<Status>, List<StatusDto>>(statuses);
            return statusDTOs;
        }

        public async Task<StatusDto> AddStatusAsync(StatusDto statusDto)
        {
            Status status = _mapper.Map<StatusDto, Status>(statusDto);
            Status newStatus = await _statusRepository.AddStatusAsync(status);
            return _mapper.Map<Status, StatusDto>(newStatus);
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
