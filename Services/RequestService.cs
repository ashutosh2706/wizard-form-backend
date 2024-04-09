using WizardFormBackend.DTOs;
using WizardFormBackend.Models;
using WizardFormBackend.Repositories;

namespace WizardFormBackend.Services
{
    public class RequestService(IRequestRepository requestRepository) : IRequestService
    {
        private readonly IRequestRepository _requestRepository = requestRepository;

        public async Task<IEnumerable<RequestDTO>> GetAllRequestAsync()
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllRequestAsync();
            List<RequestDTO> requestDTOs = new List<RequestDTO>();
            foreach (Request request in requests)
            {
                requestDTOs.Add(new RequestDTO
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
            return requestDTOs;
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

            Request request = await _requestRepository.AddRequestAsync(newRequest);
            requestDTO.RequestId = request.RequestId;
            requestDTO.StatusCode = request.StatusCode;
            return requestDTO;
        }

        public async Task UpdateRequestStatusAsync(long requestId, int statusCode)
        {
            Request? existingRequest = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if (existingRequest != null)
            {
                existingRequest.StatusCode = statusCode;    // => 2: approved 3:rejected
                await _requestRepository.UpdateRequestAsync(existingRequest);
            }
        }


        public async Task<IEnumerable<RequestDTO>> GetAllRequestByUserIdAsync(long userId)
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllRequestByUserIdAsync(userId);
            List<RequestDTO> result = new List<RequestDTO>();
            foreach (Request request in requests)
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
            return result;
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
