using WizardFormBackend.Dto;

namespace WizardFormBackend.Services
{
    public interface IRequestService
    {
        Task<RequestDto> AddRequestAsync(RequestDto requestDto);
        Task DeleteRequestAsync(long requestId);
        Task<PaginatedResponseDto<RequestDto>> GetAllRequestAsync(string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection);
        Task<PaginatedResponseDto<RequestDto>> GetAllRequestByUserIdAsync(long userId, string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection);
        Task UpdateRequestStatusAsync(long requestId, int statusCode);
        Task<RequestDto?> GetRequestByRequestIdAsync(long requestId);
    }
}