using BookStoreMVC.Utility;

namespace BookStoreMVC.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<ShoppingCartItem> Items { get; set; }
        public int ItemsQuantity => Items.Count();
        public double Subtotal => Items.Sum(x => x.Product.ListPrice * x.quantity);
        public double Vat => Subtotal - (Subtotal / (1 + Constants.Prices.Vat));
        public double Shipping => Subtotal == 0 ? 0 : Constants.Prices.Shipping;
        public double Total => Subtotal + Shipping;
    }
}
