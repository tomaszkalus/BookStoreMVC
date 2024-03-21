using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public ShoppingCartController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            CartVM cartVM = new CartVM()
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };

            return View(cartVM);
        }
    }
}
