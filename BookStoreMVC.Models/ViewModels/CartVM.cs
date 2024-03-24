using BookStoreMVC.Utility;

namespace BookStoreMVC.Models.ViewModels
{
    //TODO Use CartVM for creating CartDTO
    public class CartVM
    {
        public IEnumerable<ShoppingCartItem> Items { get; set; }

        public double Subtotal => Items.Sum(x => x.Product.ListPrice * x.quantity);
        public double Vat => Subtotal - (Subtotal / (1 + Constants.Prices.Vat));
        public double Shipping => Constants.Prices.Shipping;
        public double Total => Subtotal + Shipping;
    }
}
