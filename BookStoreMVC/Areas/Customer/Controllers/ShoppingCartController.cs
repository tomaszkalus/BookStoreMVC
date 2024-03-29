using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            CartVM cartVM = new CartVM()
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };

            return View(cartVM);
        }

        public IActionResult Summary()
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            string userId = _userManager.GetUserId(User);
            CartVM cartVM = new CartVM()
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };


            SummaryVM summaryVM = new SummaryVM()
            {
                Cart = cartVM,
                User = user
            };
            return View(summaryVM);
        }
        
        public IActionResult PlaceOrder()
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            string userId = _userManager.GetUserId(User);
            CartVM cartVM = new CartVM()
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };

            SummaryVM summaryVM = new SummaryVM()
            {
                Cart = cartVM,
                User = user
            };

            return View(summaryVM);
        }
    }
}
