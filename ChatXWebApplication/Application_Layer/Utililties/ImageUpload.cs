using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application_Layer.Utililties
{
    public class ImageUpload
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageUpload(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> ImageUploadAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            string ext = Path.GetExtension(file.FileName)
                .ToLowerInvariant();

            string[] allowedExtensions =
            {
                ".jpg", ".jpeg", ".png", ".gif", ".webp"
            };

            if (!allowedExtensions.Contains(ext))
                return null;

            string newFileName = $"{Guid.NewGuid()}{ext}";

            string webRoot = _environment.WebRootPath ?? _environment.ContentRootPath;

            string folderPath = Path.Combine(webRoot, "uploads");

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, newFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null)
                return null;

            return $"{request.Scheme}://{request.Host}/uploads/{newFileName}";
        }
    }
}
