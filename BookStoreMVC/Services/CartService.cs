using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Utility;
using Microsoft.AspNetCore.Mvc;

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

        public ServiceResult PlaceOrder(Cart cart)
        {
            Order order = new Order
            {
                OrderId = Guid.NewGuid().ToString(),
                UserId = cart.UserId,
                Date = DateTime.Now,
                Status = Constants.OrderStatus.Pending,
            };

            order.Items = cart.Items.Select(item => new OrderItem
            {
                ProductId = item.productId,
                Quantity = item.quantity,
                Price = item.Product.Price
            }).ToList();

            _unitOfWork.Order.Add(order);

            foreach (ShoppingCartItem item in cart.Items)
            {
                _unitOfWork.UserProductShoppingCart.Remove(item);
            }
            _unitOfWork.Save();
            return new ServiceResult() { Success = true, Message = "Order has been placed successfully." };
        }

    }

}

