using BookStoreMVC.Models.DTO;
using BookStoreMVC.Models.ViewModels;

namespace BookStoreMVC.Models.Mappers
{
    public static class ShoppingCartMapper
    {
        public static CartDTO MapToDto(CartVM cartVM)
        {
            return new CartDTO
            {
                Items = cartVM.Items.Select(i => MapToDto(i)),
                ItemsQuantity = cartVM.ItemsQuantity,
                Subtotal = new PriceDTO(cartVM.Subtotal),
                Shipping = new PriceDTO(cartVM.Shipping),
                Vat = new PriceDTO(cartVM.Vat),
                Total = new PriceDTO(cartVM.Total)
            };
        }

        public static CartItemDTO MapToDto(ShoppingCartItem cartItem)
        {
            return new CartItemDTO
            {
                ProductId = cartItem.productId,
                Quantity = cartItem.quantity,
                UnitPrice = new PriceDTO(cartItem.Product.ListPrice),
                TotalPrice = new PriceDTO(cartItem.TotalPrice)
            };
        }
    }
}
