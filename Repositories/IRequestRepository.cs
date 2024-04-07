using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public interface IRequestRepository
    {
        Task<Request> AddRequestAsync(Request request);
        Task DeleteRequestAsync(Request request);
        Task<IEnumerable<Request>> GetAllRequestAsync();
        Task<IEnumerable<Request>> GetAllRequestByUserIdAsync(long userId);
        Task<Request?> GetRequestByRequestIdAsync(long requestId);
        Task UpdateRequestAsync(Request request);
    }
}