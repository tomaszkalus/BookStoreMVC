using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreMVC.Areas.Customer.Views
{
    public class YourOrdersModel : PageModel
    {
        public IEnumerable<Order> orders;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public YourOrdersModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public void OnGet()
        {
            string userId = _userManager.GetUserId(User);
            orders = _unitOfWork.Order.GetAllUserOrders(userId);
        }
    }
}