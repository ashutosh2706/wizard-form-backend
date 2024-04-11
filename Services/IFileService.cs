using WizardFormBackend.Models;

namespace WizardFormBackend.Services
{
    public interface IFileService
    {
        Task<FileDetail?> AddFileAsync(IFormFile file);
    }
}