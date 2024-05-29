using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data.Context;
using WizardFormBackend.Data.Models;

namespace WizardFormBackend.Data.Repositories
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly WizardFormDbContext _dbContext;

        public PriorityRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Priority>> GetAllPriorityAsync()
        {
            return await _dbContext.Priorities.ToListAsync();
        }

        public async Task<Priority?> GetPriorityByPriorityCodeAsync(int priorityCode)
        {
            return await _dbContext.Priorities.FindAsync(priorityCode);
        }

        public async Task<Priority> AddPriorityAsync(Priority priority)
        {
            _dbContext.Priorities.Add(priority);
            await _dbContext.SaveChangesAsync();
            return priority;
        }

        public async Task UpdatePriorityAsync(Priority priority)
        {
            _dbContext.Priorities.Update(priority);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePriorityAsync(Priority priority)
        {
            _dbContext.Priorities.Remove(priority);
            await _dbContext.SaveChangesAsync();
        }
    }
}
