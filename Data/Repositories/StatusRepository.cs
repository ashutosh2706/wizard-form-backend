using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data.Context;
using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly WizardFormDbContext _dbContext;
        public StatusRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            return await _dbContext.Statuses.ToListAsync();
        }

        public async Task<Status?> GetStatusByStatusCodeAsync(int statusCode)
        {
            return await _dbContext.Statuses.FindAsync(statusCode);
        }

        public async Task<Status> AddStatusAsync(Status status)
        {
            _dbContext.Statuses.Add(status);
            await _dbContext.SaveChangesAsync();
            return status;
        }

        public async Task UpdateStatusAsync(Status status)
        {
            _dbContext.Statuses.Update(status);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStatusAsync(Status status)
        {
            _dbContext.Statuses.Remove(status);
            await _dbContext.SaveChangesAsync();
        }
    }
}
