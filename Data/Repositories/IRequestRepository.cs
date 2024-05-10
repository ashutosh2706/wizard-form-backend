using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Repositories
{
    public interface IRequestRepository
    {
        Task<Request> AddRequestAsync(Request request);
        Task DeleteRequestAsync(Request request);
        Task<IEnumerable<Request>> GetAllRequestAsync(string searchKeyword);
        Task<IEnumerable<Request>> GetAllRequestByUserIdAsync(long userId, string searchKeyword);
        Task<Request?> GetRequestByRequestIdAsync(long requestId);
        Task UpdateRequestAsync(Request request);
    }
}