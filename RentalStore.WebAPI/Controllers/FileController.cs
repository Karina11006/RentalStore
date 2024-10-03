using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RentalStore.Application.Controllers
{
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult DownloadFile([FromQuery] string fileName)
        {
            var dir = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, "Files", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, mimeType, fileName);
        }

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            var dir = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(dir, "Files", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok();
        }
    }
}
