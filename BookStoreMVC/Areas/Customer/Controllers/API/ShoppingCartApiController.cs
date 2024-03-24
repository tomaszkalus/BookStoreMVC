using Ajax;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Models.DTO;
using BookStoreMVC.Models.Mappers;
using BookStoreMVC.Models.ViewModels;
using BookStoreMVC.Services;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Areas.Customer.Controllers.API
{
    [Route("api/user/cart")]
    [ApiController]
    [Authorize(Roles = SD.Role_Cust + "," + SD.Role_Admin)]
    public class ShoppingCartApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartApiController(IUnitOfWork unitOfWork, ICartService cartService, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _cartService = cartService;
        }

        [HttpGet]
        public ActionResult<JSend> GetCart()
        {
            string? userId = _userManager.GetUserId(HttpContext.User);
            CartVM cartVM = new CartVM
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(cartVM);
            return Ok(JSend.Success(cartDTO));
        }

        // GET api/user/cart
        [HttpGet("item/{productId}")]
        public ActionResult<JSend> GetCart(int productId)
        {
            if (_unitOfWork.Product.GetById(productId) == null)
            {
                return NotFound(JSend.Fail("Product does not exist"));
            }
            string? userId = _userManager.GetUserId(HttpContext.User);
            ShoppingCartItem? shoppingCartItem = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == productId);

            if (shoppingCartItem == null)
            {
                return NotFound(JSend.Fail("Item is not present in the cart"));
            }

            CartItemDTO cartItemDTO = ShoppingCartMapper.MapToDto(shoppingCartItem);
            return Ok(JSend.Success(cartItemDTO));
        }

        // POST api/user/cart
        [HttpPost]
        public ActionResult<JSend> AddItemToCart([FromBody] ProductDTO newCartItem)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            ServiceResult result = _cartService.AddItem(newCartItem.ProductId, newCartItem.Quantity, userId);
            if (!result.Success)
            {
                return NotFound(JSend.Fail(result.Message));
            }

            CartVM cartVM = new CartVM
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(cartVM);
            return Ok(JSend.Success(cartDTO));
        }

        // PUT api/user/cart/item/5
        [HttpPut("item/{id}")]
        public ActionResult<JSend> UpdateCartItem(int id, [FromBody] int quantity)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            ServiceResult result = _cartService.UpdateQuantity(id, quantity, userId);

            if (!result.Success)
            {
                return NotFound(JSend.Fail(result.Message));
            }
            CartVM cartVM = new CartVM
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(cartVM);
            return Ok(JSend.Success(cartDTO));
        }

        // DELETE api/user/cart/item/5
        [HttpDelete("item/{id}")]
        public ActionResult<JSend> DeleteCartItem(int id)
        {
            Product? product = _unitOfWork.Product.GetById(id);

            if (product == null)
            {
                return NotFound(JSend.Fail("Product does not exist"));
            }

            string? userId = _userManager.GetUserId(HttpContext.User);
            ShoppingCartItem? shoppingCartItem = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == id);

            if (shoppingCartItem == null)
            {
                return NotFound(JSend.Fail("Item is not present in the cart"));
            }

            _unitOfWork.UserProductShoppingCart.Remove(shoppingCartItem);
            _unitOfWork.Save();

            CartVM cartVM = new CartVM
            {
                Items = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
            };
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(cartVM);
            return Ok(JSend.Success(cartDTO));
        }
    }
}
