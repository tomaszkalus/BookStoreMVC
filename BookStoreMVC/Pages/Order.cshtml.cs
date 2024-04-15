using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreMVC.Areas.Customer.Pages
{
    [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
    public class OrderModel : PageModel
    {
        public Order order;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult OnGet(int id)
        {
            string userId = _userManager.GetUserId(User);
            order = _unitOfWork.Order.GetOrder(id);
            if(order == null || order.UserId != userId)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
