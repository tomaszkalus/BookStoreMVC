using BookStoreMVC.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.Mappers
{
    public static class ShoppingCartMapper
    {
        public static CartDTO MapToDto(IEnumerable<ShoppingCartItem> cartItems)
        {
            return new CartDTO
            {
                Items = cartItems.Select(i => MapToDto(i)),
                ItemsQuantity = cartItems.Count(),
                TotalPrice = cartItems.Sum(i => i.Product.ListPrice * i.quantity)
            };
        }

        public static CartItemDTO MapToDto(ShoppingCartItem cartItem)
        {
            return new CartItemDTO
            {
                ProductId = cartItem.productId,
                Quantity = cartItem.quantity,
                UnitPrice = cartItem.Product.ListPrice,
                TotalPrice = cartItem.TotalPrice
            };
        }
    }
}
