namespace BookStoreMVC.Models.DTO
{
    public class CartDTO
    {
        public IEnumerable<CartItemDTO> Items { get; set; }
        public int ItemsQuantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
