using BookStoreMVC.Utility;

namespace BookStoreMVC.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<ShoppingCartItem> Items { get; set; }
        public int ItemsQuantity => Items.Count();
        public decimal Subtotal => Items.Sum(x => x.Product.Price * x.quantity);
        public decimal Vat => Subtotal - (Subtotal / (1 + Constants.Prices.Vat));
        public decimal Shipping => Subtotal == 0 ? 0 : Constants.Prices.Shipping;
        public decimal Total => Subtotal + Shipping;
    }
}
