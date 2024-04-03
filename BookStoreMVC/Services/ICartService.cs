using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;

namespace BookStoreMVC.Services
{
    public interface ICartService
    {
        ServiceResult AddItem(int productId, int quantity, string userId);
        ServiceResult PlaceOrder(SummaryVM summaryVM);
        ServiceResult UpdateQuantity(int productId, int quantity, string? userId);
    }
}