using BookStoreMVC.Models.JsonModels;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
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

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                IEnumerable<UserProductShoppingCart> userCart = _unitOfWork.UserProductShoppingCart.GetByUserId(userId);
                return View(userCart);
            }
            return RedirectToAction(controllerName: "Identity", actionName: "Login");


        }


        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart([FromBody] NewCartItem newCartItem)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(401);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Json(new { success = false, message = "The user isn't authenticated" });
            }

            UserProductShoppingCart? itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id)
                .FirstOrDefault(p => p.productId == newCartItem.ProductId);

            if (itemInCart != null)
            {
                itemInCart.quantity += newCartItem.Quantity;
                _unitOfWork.UserProductShoppingCart.Update(itemInCart);
            }
            else
            {
                UserProductShoppingCart shoppingCartItem = new UserProductShoppingCart()
                {
                    userId = currentUser.Id,
                    productId = newCartItem.ProductId,
                    quantity = newCartItem.Quantity
                };

                _unitOfWork.UserProductShoppingCart.Add(shoppingCartItem);
            }

            _unitOfWork.Save();
            return Json(new { success = true, message = "Adding to cart Successful!" });
        }

        public async Task<IActionResult> GetCartAmount()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return StatusCode(401);
            }

            int cartAmount = _unitOfWork.UserProductShoppingCart.GetShoppingCartProductsAmount(currentUser.Id);

            return Json(new { success = true, cartAmount });
        }

        public async Task<IActionResult> UpdateItemQuantity([FromBody] NewCartItem newCartItem)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(401);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Json(new { success = false, message = "The user isn't authenticated" });
            }

            UserProductShoppingCart? itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id)
                .FirstOrDefault(p => p.productId == newCartItem.ProductId);

            if (itemInCart != null)
            {
                _unitOfWork.UserProductShoppingCart.Update(itemInCart);
                _unitOfWork.Save();
                return Json(new { success = true, message = "The item quantity was updated" });
            }


            return Json(new { success = false, message = "The item is not in the cart" });


        }
    }
}
