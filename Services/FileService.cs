using System.Net.Http.Headers;
using WizardFormBackend.Data.Models;
using WizardFormBackend.Data.Repositories;
using WizardFormBackend.Utils;

namespace WizardFormBackend.Services
{
    public class FileService(IFileDetailRepository fileDetailRepository) : IFileService
    {
        private readonly IFileDetailRepository _fileDetailRepository = fileDetailRepository;


        public async Task<FileDetail?> AddFileAsync(IFormFile file)
        {

            string originalName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName ?? "".Trim('"');
            originalName = Util.ReplaceSpecialChars(originalName);

            FileDetail fileDetail = new()
            {
                FileName = originalName,
                Checksum = Util.CalculateChecksum(file)
            };
          
            FileDetail? savedFileDetail = await _fileDetailRepository.AddFileDetailAsync(fileDetail);
            if (savedFileDetail != null)
            {

                string formattedName = $"{originalName}.{savedFileDetail.FileId}";
                string filePath = Path.Combine("FileUploads", formattedName);
                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);

                return savedFileDetail;
            }

            return null;
        }
    }
}
