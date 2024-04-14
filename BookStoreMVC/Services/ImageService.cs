using BookStoreMVC.Utility;
using SixLabors.ImageSharp;

namespace BookStoreMVC.Services
{
    public class ImageService : IImageService
    {
        public void DeleteIfExists(string rootPath, string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;
            var oldImagePath = Path.Combine(rootPath, imagePath.TrimStart('\\'));
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }

    }


}
