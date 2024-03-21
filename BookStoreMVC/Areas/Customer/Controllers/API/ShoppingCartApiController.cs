using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Models.DTO;
using BookStoreMVC.Models.Mappers;
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

        // GET api/user/cart
        [HttpGet]
        public ActionResult<CartDTO> GetCart()
        {
            string? userId = _userManager.GetUserId(HttpContext.User);
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(_unitOfWork.UserProductShoppingCart.GetByUserId(userId));
            return Ok(cartDTO);
        }

        // GET api/user/cart
        [HttpGet("item/{itemId}")]
        public ActionResult<CartItemDTO> GetCart(int itemId)
        {
            string? userId = _userManager.GetUserId(HttpContext.User);
            ShoppingCartItem? shoppingCartItem = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == itemId);

            if (shoppingCartItem == null)
            {
                return NotFound("Item not found");
            }

            CartItemDTO cartItemDTO = ShoppingCartMapper.MapToDto(shoppingCartItem);
            return Ok(cartItemDTO);
        }

        // POST api/user/cart
        [HttpPost]
        public ActionResult<CartDTO> AddItemToCart([FromBody] ProductDTO newCartItem)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            ServiceResult result = _cartService.AddItem(newCartItem.ProductId, newCartItem.Quantity, userId);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            CartDTO cartDTO = ShoppingCartMapper.MapToDto(_unitOfWork.UserProductShoppingCart.GetByUserId(userId));
            return Ok(cartDTO);
        }

        // PUT api/user/cart/item/5
        [HttpPut("item/{id}")]
        public ActionResult<CartDTO> UpdateCartItem(int id, [FromBody] int quantity)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            ServiceResult result = _cartService.UpdateQuantity(id, quantity, userId);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            CartDTO cartDTO = ShoppingCartMapper.MapToDto(_unitOfWork.UserProductShoppingCart.GetByUserId(userId));
            return Ok(cartDTO);
        }

        // DELETE api/user/cart/item/5
        [HttpDelete("item/{id}")]
        public ActionResult<CartDTO> DeleteCartItem(int id)
        {
            Product? product = _unitOfWork.Product.GetById(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            string? userId = _userManager.GetUserId(HttpContext.User);
            ShoppingCartItem? shoppingCartItem = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == id);

            if (shoppingCartItem == null)
            {
                return NotFound("Item not found");
            }

            _unitOfWork.UserProductShoppingCart.Remove(shoppingCartItem);
            _unitOfWork.Save();

            CartDTO cartDTO = ShoppingCartMapper.MapToDto(_unitOfWork.UserProductShoppingCart.GetByUserId(userId));
            return Ok(cartDTO);
        }
    }
}
