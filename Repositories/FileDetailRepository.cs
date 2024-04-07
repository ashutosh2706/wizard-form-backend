using Microsoft.EntityFrameworkCore;
using WizardFormBackend.Data;
using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public class FileDetailRepository : IFileDetailRepository
    {
        private readonly WizardFormDbContext _dbContext;
        public FileDetailRepository(WizardFormDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FileDetail>> GetAllFileDetailAsync()
        {
            return await _dbContext.FileDetails.ToListAsync();
        }

        public async Task<FileDetail?> GetRoleByRoleIdAsync(long fileId)
        {
            return await _dbContext.FileDetails.FindAsync(fileId);
        }

        public async Task<FileDetail> AddFileDetailAsync(FileDetail fileDetail)
        {
            _dbContext.FileDetails.Add(fileDetail);
            await _dbContext.SaveChangesAsync();
            return fileDetail;
        }

        public async Task UpdateFileDetailAsync(FileDetail fileDetail)
        {
            _dbContext.FileDetails.Update(fileDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFileDetailAsync(FileDetail fileDetail)
        {
            _dbContext.FileDetails.Remove(fileDetail);
            await _dbContext.SaveChangesAsync();
        }
    }
}
