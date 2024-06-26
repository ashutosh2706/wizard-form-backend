﻿using WizardFormBackend.Data.Dto;

namespace WizardFormBackend.Services
{
    public interface IRequestService
    {
        Task<RequestDto> AddRequestAsync(RequestDto requestDto);
        Task<bool> DeleteRequestAsync(long requestId);
        Task<PagedResponseDto<RequestDto>> GetAllRequestAsync(string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection);
        Task<PagedResponseDto<RequestDto>> GetAllRequestByUserIdAsync(long userId, string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection);
        Task<bool> UpdateRequestStatusAsync(long requestId, int statusCode);
        Task<RequestDto?> GetRequestByRequestIdAsync(long requestId);
    }
}