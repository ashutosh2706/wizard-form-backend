using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public interface IRequestService
    {
        Task<RequestDTO> AddRequestAsync(RequestDTO requestDTO);
        Task DeleteRequestAsync(long requestId);
        Task<IEnumerable<RequestDTO>> GetAllRequestAsync();
        Task<IEnumerable<RequestDTO>> GetAllRequestByUserIdAsync(long userId);
        Task UpdateRequestStatusAsync(long requestId, int statusCode);
        Task<RequestDTO?> GetRequestByRequestIdAsync(long requestId);
    }
}