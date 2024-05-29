using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core;
using System.Reflection;
using WizardFormBackend.Data.Dto;
using WizardFormBackend.Data.Models;
using WizardFormBackend.Data.Repositories;

namespace WizardFormBackend.Services
{
    public class RequestService(IRequestRepository requestRepository, IFileService fileService, IMapper mapper) : IRequestService
    {
        private readonly IRequestRepository _requestRepository = requestRepository;
        private readonly IFileService _fileService = fileService;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResponseDto<RequestDto>> GetAllRequestAsync(string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection)
        {
            IEnumerable<Request> requests = await _requestRepository.GetAllRequestAsync(searchTerm);

            PropertyInfo[] properties = typeof(Request).GetProperties();

            string field;
            if(!sortField.IsNullOrEmpty())
            {
                var matchedField = properties.FirstOrDefault(p => p.Name.Equals(sortField, StringComparison.CurrentCultureIgnoreCase));
                if(matchedField == null)
                {
                    throw new Exception($"Invalid sort field: {sortField}");
                }
                else
                {
                    field = matchedField.Name;
                }
            }
            else
            {
                field = "RequestId";
            }

            string sortExpression = sortDirection == "descending" ? $"{field} descending" : $"{field} ascending";
            IEnumerable<Request> sortedRequests = requests.AsQueryable().OrderBy(sortExpression).AsEnumerable();
            int totalPage = (int)Math.Ceiling((decimal)sortedRequests.Count() / pageSize);
            IEnumerable<Request> paginatedRequests = sortedRequests.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<RequestDto> result = _mapper.Map<IEnumerable<Request>, List<RequestDto>>(paginatedRequests);
            return new PagedResponseDto<RequestDto> { PageNumber = pageNumber, TotalPage = totalPage, PageSize = pageSize, Items = result };
        }

        public async Task<RequestDto> AddRequestAsync(RequestDto requestDto)
        {
            Request newRequest = _mapper.Map<RequestDto, Request>(requestDto);
            newRequest.StatusCode = 1;
            IFormFile? file = requestDto.AttachedFile;

            if(file != null)
            {
                FileDetail? savedFileDetail = await _fileService.AddFileAsync(file);
                if(savedFileDetail != null)
                {
                    newRequest.FileId = savedFileDetail.FileId;
                }
            }
            
            Request savedRequest = await _requestRepository.AddRequestAsync(newRequest);
            requestDto.RequestId = savedRequest.RequestId;
            requestDto.StatusCode = savedRequest.StatusCode;
            return requestDto;
        }

        public async Task<bool> UpdateRequestStatusAsync(long requestId, int statusCode)
        {
            Request? existingRequest = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if (existingRequest != null)
            {
                existingRequest.StatusCode = statusCode;
                await _requestRepository.UpdateRequestAsync(existingRequest);
                return true;
            }

            return false;
        }


        public async Task<PagedResponseDto<RequestDto>> GetAllRequestByUserIdAsync(long userId, string searchTerm, int pageNumber, int pageSize, string sortField, string sortDirection)
        {

            IEnumerable<Request> requests = await _requestRepository.GetAllRequestByUserIdAsync(userId, searchTerm);        // get the filterd requests from repository
            PropertyInfo[] properties = typeof(Request).GetProperties();

            string field;
            if (!sortField.IsNullOrEmpty())
            {
                var matchedField = properties.FirstOrDefault(p => p.Name.Equals(sortField, StringComparison.CurrentCultureIgnoreCase));
                if (matchedField == null)
                {
                    throw new Exception($"Invalid sort field: {sortField}");
                }
                else
                {
                    field = matchedField.Name;
                }
            }
            else
            {
                field = "RequestId";
            }

            string sortExpression = sortDirection == "descending" ? $"{field} descending" : $"{field} ascending";   // fallback to default sort direction
            IEnumerable<Request> sortedRequests = requests.AsQueryable().OrderBy(sortExpression).AsEnumerable();
            int totalPage = (int)Math.Ceiling((decimal)sortedRequests.Count() / pageSize);
            IEnumerable<Request> paginatedRequests = sortedRequests.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            List<RequestDto> result = _mapper.Map<IEnumerable<Request>, List<RequestDto>>(paginatedRequests);
            return new PagedResponseDto<RequestDto> { PageNumber = pageNumber, TotalPage = totalPage, PageSize = pageSize, Items = result };
        }

        public async Task<RequestDto?> GetRequestByRequestIdAsync(long requestId)
        {
            Request? request = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if(request != null)
            {
                RequestDto requestDto = _mapper.Map<Request, RequestDto>(request);
                requestDto.Phone = request.Phone ?? "";
                return requestDto;
            }
            return null;
        }

        public async Task<bool> DeleteRequestAsync(long requestId)
        {
            Request? existingRequest = await _requestRepository.GetRequestByRequestIdAsync(requestId);
            if (existingRequest != null)
            {
                await _requestRepository.DeleteRequestAsync(existingRequest);
                return true;
            }

            return false;
        }
    }
}
