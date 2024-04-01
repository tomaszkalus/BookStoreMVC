using BookStoreMVC.Models;

namespace BookStoreMVC.Services
{
    public interface ICartService
    {
        ServiceResult AddItem(int productId, int quantity, string userId);
        ServiceResult PlaceOrder(Cart cart);
        ServiceResult UpdateQuantity(int productId, int quantity, string? userId);
    }
}