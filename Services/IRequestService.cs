using WizardFormBackend.DTOs;
using WizardFormBackend.DTOs.Paginated;

namespace WizardFormBackend.Services
{
    public interface IRequestService
    {
        Task<RequestDTO> AddRequestAsync(RequestDTO requestDTO);
        Task DeleteRequestAsync(long requestId);
        Task<PaginatedRequestDTO> GetAllRequestAsync(string query, int page, int limit);
        Task<PaginatedRequestDTO> GetAllRequestByUserIdAsync(long userId, string query, int page, int limit);
        Task UpdateRequestStatusAsync(long requestId, int statusCode);
        Task<RequestDTO?> GetRequestByRequestIdAsync(long requestId);
    }
}