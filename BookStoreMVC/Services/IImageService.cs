
namespace BookStoreMVC.Services
{
    public interface IImageService
    {
        void DeleteIfExists(string rootPath, string? imagePath);
    }
}
