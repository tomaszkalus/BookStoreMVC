using BookStoreMVC.Utility;
using System.Reflection.Metadata;

namespace BookStoreMVC.Models.DTO
{
    public class CartDTO
    {
        public IEnumerable<CartItemDTO> Items { get; set; }
        public int ItemsQuantity { get; set; }
        public PriceDTO Subtotal { get; set; }
        public PriceDTO Shipping { get; set; }
        public PriceDTO Vat { get; set; }
        public PriceDTO Total { get; set; }
    }
}
