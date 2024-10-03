using Microsoft.AspNetCore.Components.Forms;

namespace RentalStore.BlazorServer.Services
{
    public interface IFileService
    {
        bool DeleteFile(string filePath);
        Task<string> UploadFile(IBrowserFile file);
    }

    public class FileService : IFileService
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool DeleteFile(string filePath)
        {
            var absolutePath = _webHostEnvironment.WebRootPath + filePath;
            if (!File.Exists(absolutePath))
            {
                return false;
            }

            File.Delete(absolutePath);
            return true;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new FileInfo(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var dir = $"{_webHostEnvironment.WebRootPath}\\images";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, fileName);

            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            var fullPath = $"/images/{fileName}";
            return fullPath;
        }
    }
}
