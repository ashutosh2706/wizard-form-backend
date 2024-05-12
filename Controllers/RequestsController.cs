using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WizardFormBackend.Data.Dto;
using WizardFormBackend.Services;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllRequest([FromQuery]QueryParams queryParams)
        {
            PagedResponseDto<RequestDto> response = await _requestService.GetAllRequestAsync(queryParams.SearchTerm, queryParams.PageNumber, queryParams.PageSize, queryParams.SortField, queryParams.SortDirection);
            return Ok(response);
        }

        [HttpGet("user/{UserId}")]
        public async Task<IActionResult> GetAllRequestByUserId(long UserId, [FromQuery]QueryParams queryParams)
        {
            PagedResponseDto<RequestDto> response = await _requestService.GetAllRequestByUserIdAsync(UserId, queryParams.SearchTerm, queryParams.PageNumber, queryParams.PageSize, queryParams.SortField, queryParams.SortDirection);
            return Ok(response);
        }

        [HttpGet("{RequestId}")]
        public async Task<IActionResult> GetRequestByRequestId(long RequestId)
        {
            RequestDto? requestDTO = await _requestService.GetRequestByRequestIdAsync(RequestId);
            return requestDTO != null ? Ok(requestDTO) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddRequest([FromForm] RequestDto requestDto)
        {
            RequestDto? response = await _requestService.AddRequestAsync(requestDto);
            return Created("/Requests", response);
        }

        [HttpPut("update/{RequestId}/{StatusCode}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRequestStatus(long RequestId, int StatusCode)
        {
            bool actionPerformed = await _requestService.UpdateRequestStatusAsync(RequestId, StatusCode);
            return actionPerformed ? Ok() : NotFound();
        }

        [HttpDelete("delete/{RequestId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteRequest(long RequestId)
        {
            bool actionPerformed = await _requestService.DeleteRequestAsync(RequestId);
            return actionPerformed ? NoContent() : NotFound();
        }
    }
}
