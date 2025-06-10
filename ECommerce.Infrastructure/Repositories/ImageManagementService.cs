using ECommerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace ECommerce.Infrastructure.Repositories
{
    public class ImageManagementService(IFileProvider fileProvider) : IImageManagementService
    {
        private readonly IFileProvider _fileProvider = fileProvider;

        public async Task<List<string>> AddImage(IFormFileCollection files, string src)
        {
            List<string> savedImagesSrc = [];
            string imageDirctory = Path.Combine("wwwroot", "Images", src);
            if (!File.Exists(imageDirctory))
                Directory.CreateDirectory(imageDirctory);
            foreach (var file in files)
            {
                string imageName = file.FileName;
                string imageSrc = $"/Images/{src}/{imageName}";
                string root = Path.Combine(imageDirctory, imageName);

                using FileStream stream = new(root, FileMode.Create);
                await file.CopyToAsync(stream);

                savedImagesSrc.Add(imageSrc);
            }
            return savedImagesSrc;
        }

        public void DeleteImage(string src)
        {
            IFileInfo fileInfo = _fileProvider.GetFileInfo(src);
            string? root = fileInfo.PhysicalPath;
            if (File.Exists(root))
                File.Delete(root);

        }
    }
}
