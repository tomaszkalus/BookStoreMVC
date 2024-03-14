using BookStoreMVC.Models.JsonModels;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
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

        // API CALLS

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart([FromBody] ProductDTO newCartItem)
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

        [HttpPost]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] ProductDTO cartItem)
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

            var itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id).Single(u => u.productId == cartItem.ProductId);

            if (itemInCart != null)
            {
                itemInCart.quantity = cartItem.Quantity;
                _unitOfWork.UserProductShoppingCart.Update(itemInCart);
                _unitOfWork.Save();
                return Json(new { success = true, message = "The item quantity was updated" });
            }


            return Json(new { success = false, message = "The item is not in the cart" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> RemoveCartItem(int id)
        {

            var itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(_userManager.GetUserId(User)).Single(u => u.productId == id);
            if (itemInCart != null)
            {
                _unitOfWork.UserProductShoppingCart.Remove(itemInCart);
                _unitOfWork.Save();
                return Json(new { success = true, message = "The product has been removed from the cart" });
            }
            return Json(new { success = false, message = "There was an error when removing from cart" });
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ItemQuantity(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return StatusCode(401);
            }

            var itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id).Single(u => u.productId == id);
            if (itemInCart != null)
            {
                return Json(new { success = true, quantity = itemInCart.quantity });
            }
            return Json(new { success = false, message = "There was an error when getting the quantity" });
        }


    }
}
