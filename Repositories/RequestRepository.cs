using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data;
using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly WizardFormDbContext _dbContext;
        public RequestRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Request>> GetAllRequestAsync()
        {
            return await _dbContext.Requests.ToListAsync();
        }

        public async Task<Request?> GetRequestByRequestIdAsync(long requestId)
        {
            return await _dbContext.Requests.FindAsync(requestId);
        }

        public async Task<IEnumerable<Request>> GetAllRequestByUserIdAsync(long userId)
        {
            return await _dbContext.Requests.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Request> AddRequestAsync(Request request)
        {
            _dbContext.Requests.Add(request);
            await _dbContext.SaveChangesAsync();
            return request;
        }

        public async Task UpdateRequestAsync(Request request)
        {
            _dbContext.Requests.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRequestAsync(Request request)
        {
            _dbContext.Requests.Remove(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}
