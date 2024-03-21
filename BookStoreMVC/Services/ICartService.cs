namespace BookStoreMVC.Services
{
    public interface ICartService
    {
        ServiceResult AddItem(int productId, int quantity, string userId);
        ServiceResult UpdateQuantity(int productId, int quantity, string? userId);
    }
}