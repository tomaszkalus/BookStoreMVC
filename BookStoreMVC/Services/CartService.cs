using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;

namespace BookStoreMVC.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResult AddItem(int productId, int quantity, string userId)
        {
            Product product = _unitOfWork.Product.GetById(productId);
            if (product == null)
            {
                return new ServiceResult() { Success = false, Message = "There was an error with adding product to cart." };
            }

            ShoppingCartItem? itemInCart = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == productId);

            if (itemInCart != null)
            {
                itemInCart.quantity += quantity;
                _unitOfWork.UserProductShoppingCart.Update(itemInCart);
            }
            else
            {
                ShoppingCartItem shoppingCartItem = new()
                {
                    productId = productId,
                    quantity = quantity,
                    userId = userId
                };
                _unitOfWork.UserProductShoppingCart.Add(shoppingCartItem);
            }
            _unitOfWork.Save();
            return new ServiceResult() { Success = true, Message = "Product has been added to cart" };
        }

        public ServiceResult UpdateQuantity(int productId, int quantity, string userId)
        {
            ShoppingCartItem shoppingCartItem = _unitOfWork.UserProductShoppingCart.GetByUserId(userId)
                .FirstOrDefault(p => p.productId == productId);

            if (shoppingCartItem == null)
            {
                return new ServiceResult() { Success = false, Message = "Product was not found in the cart" };
            }

            shoppingCartItem.quantity = quantity;
            _unitOfWork.UserProductShoppingCart.Update(shoppingCartItem);
            _unitOfWork.Save();
            return new ServiceResult() { Success = true, Message = "Cart updated" };
        }

    }
}
