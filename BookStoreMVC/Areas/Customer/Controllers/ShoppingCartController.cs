using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Services;
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
        private readonly ICartService _cartService;
        public ShoppingCartController(IUnitOfWork unitOfWork, ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            Cart cartVM = new Cart()
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };

            return View(cartVM);
        }

        public IActionResult Summary()
        {

            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            string userId = _userManager.GetUserId(User);
            Cart cartVM = new Cart()
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

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            string userId = _userManager.GetUserId(User);
            Cart cart = _unitOfWork.ShoppingCart.GetCart(userId);

            ServiceResult result = _cartService.PlaceOrder(cart);
            if (result.Success == false)
            {
                TempData["error"] = "Error while placing order!";
                return RedirectToAction("Summary");
            }
            
            TempData["success"] = "Order has been placed successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
