using BookStoreMVC.Models.JsonModels;
using BookStoreMVC.Utility;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
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

        // API CALLS

        [HttpPost]
        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
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

            ShoppingCartItem? itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id)
                .FirstOrDefault(p => p.productId == newCartItem.ProductId);

            if (itemInCart != null)
            {
                itemInCart.quantity += newCartItem.Quantity;
                _unitOfWork.UserProductShoppingCart.Update(itemInCart);
            }
            else
            {
                ShoppingCartItem shoppingCartItem = new ShoppingCartItem()
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

        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
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
        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
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
        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
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

        [HttpGet]
        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
        public async Task<IActionResult> ItemTotalPrice(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return StatusCode(401);
            }

            var itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id).Single(u => u.productId == id);
            if (itemInCart != null)
            {
                return Json(new { success = true, totalPrice = itemInCart.TotalPrice.ToString("c") });
            }
            return Json(new { success = false, message = "There was an error when getting the quantity" });
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
        public async Task<IActionResult> TotalPrices()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return StatusCode(401);
            }

            var cartItems = _unitOfWork.UserProductShoppingCart.GetByUserId(currentUser.Id);
            double subtotal = cartItems.Sum(item => item.TotalPrice);
            double vat = subtotal - (subtotal / (1 + Constants.Prices.Vat));
            double shipping = Constants.Prices.Shipping;
            double total = subtotal + shipping;


            return Json(new
            {
                success = true,
                subtotal = subtotal.ToString("c"),
                vat = vat.ToString("c"),
                shipping = shipping.ToString("c"),
                total = total.ToString("c")
            });


        }


    }
}
