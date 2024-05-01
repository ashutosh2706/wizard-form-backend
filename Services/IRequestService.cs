using WizardFormBackend.DTOs;

namespace WizardFormBackend.Services
{
    public interface IRequestService
    {
        Task<RequestDTO> AddRequestAsync(RequestDTO requestDTO);
        Task DeleteRequestAsync(long requestId);
        Task<PaginatedResponseDTO<RequestDTO>> GetAllRequestAsync(string searchTerm, int pageNumber, int pageSize);
        Task<PaginatedResponseDTO<RequestDTO>> GetAllRequestByUserIdAsync(long userId, string searchTerm, int pageNumber, int pageSize);
        Task UpdateRequestStatusAsync(long requestId, int statusCode);
        Task<RequestDTO?> GetRequestByRequestIdAsync(long requestId);
    }
}