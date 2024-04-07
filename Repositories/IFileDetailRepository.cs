using WizardFormBackend.Models;

namespace WizardFormBackend.Repositories
{
    public interface IFileDetailRepository
    {
        Task<FileDetail> AddFileDetailAsync(FileDetail fileDetail);
        Task DeleteFileDetailAsync(FileDetail fileDetail);
        Task<IEnumerable<FileDetail>> GetAllFileDetailAsync();
        Task<FileDetail?> GetRoleByRoleIdAsync(long fileId);
        Task UpdateFileDetailAsync(FileDetail fileDetail);
    }
}