using WizardFormBackend.DTOs;
using WizardFormBackend.DTOs.Paginated;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class RequestService(IRequestRepository requestRepository, IFileService fileService) : IRequestService
    {
        private readonly IRequestRepository _requestRepository = requestRepository;
        private readonly IFileService _fileService = fileService;

        public async Task<PaginatedRequestDTO> GetAllRequestAsync(string query, int page, int limit)
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllRequestAsync(query);

            int totalPage = (int)Math.Ceiling((decimal)requests.Count() / limit);
            IEnumerable<Request> paginatedRequests = requests.Skip((page - 1) * limit).Take(limit).ToList();

            List<RequestDTO> result = [];
            foreach (Request request in paginatedRequests)
            {
                result.Add(new RequestDTO
                {
                    RequestId = request.RequestId,
                    UserId = request.UserId,
                    Title = request.Title,
                    GuardianName = request.GuardianName,
                    RequestDate = request.RequestDate,
                    PriorityCode = request.PriorityCode,
                    StatusCode = request.StatusCode
                });
            }
            return new PaginatedRequestDTO { Page = page, Limit = limit, Total = totalPage, Requests = result };
        }

        public async Task<RequestDTO> AddRequestAsync(RequestDTO requestDTO)
        {
            Request newRequest = new()
            {
                Title = requestDTO.Title,
                UserId = requestDTO.UserId,
                GuardianName = requestDTO.GuardianName,
                RequestDate = requestDTO.RequestDate,
                Phone = requestDTO.Phone,
                PriorityCode = requestDTO.PriorityCode,
                StatusCode = 1
            };

            IFormFile? file = requestDTO.AttachedFile;

            
            if(file != null)
            {
                FileDetail? savedFileDetail = await _fileService.AddFileAsync(file);
                if(savedFileDetail != null)
                {
                    newRequest.FileId = savedFileDetail.FileId;
                }
            }
            
            Request savedRequest = await _requestRepository.AddRequestAsync(newRequest);
            requestDTO.RequestId = savedRequest.RequestId;
            requestDTO.StatusCode = savedRequest.StatusCode;
            return requestDTO;
        }

        public async Task UpdateRequestStatusAsync(long requestId, int statusCode)
        {
            Request? existingRequest = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if (existingRequest != null)
            {
                existingRequest.StatusCode = statusCode;
                await _requestRepository.UpdateRequestAsync(existingRequest);
            }
        }


        public async Task<PaginatedRequestDTO> GetAllRequestByUserIdAsync(long userId, string filterQuery, int page, int limit)
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllRequestByUserIdAsync(userId, filterQuery);
            int totalPage = (int)Math.Ceiling((decimal)requests.Count() / limit);
            IEnumerable<Request> paginatedRequests = requests.Skip((page - 1) * limit).Take(limit).ToList();

            List<RequestDTO> result = [];
            foreach (Request request in paginatedRequests)
            {
                result.Add(new RequestDTO
                {
                    RequestId = request.RequestId,
                    UserId = request.UserId,
                    Title = request.Title,
                    RequestDate = request.RequestDate,
                    PriorityCode = request.PriorityCode,
                    StatusCode = request.StatusCode
                });
            }
            
            return new PaginatedRequestDTO { Page = page, Limit = limit, Total = totalPage, Requests = result };
        }

        public async Task<RequestDTO?> GetRequestByRequestIdAsync(long requestId)
        {
            Request? request = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if(request != null)
            {
                return new RequestDTO()
                {
                    RequestId = request.RequestId,
                    UserId = request.UserId,
                    Title = request.Title,
                    RequestDate = request.RequestDate,
                    PriorityCode = request.PriorityCode,
                    StatusCode = request.StatusCode,
                    GuardianName = request.GuardianName,
                    Phone = request.Phone ?? ""
                };
            }

            return null;
        }

        public async Task DeleteRequestAsync(long requestId)
        {
            Request? existingRequest = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if (existingRequest != null)
            {
                await _requestRepository.DeleteRequestAsync(existingRequest);
            }
        }
    }
}
