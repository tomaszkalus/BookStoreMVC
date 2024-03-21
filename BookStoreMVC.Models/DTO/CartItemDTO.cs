namespace BookStoreMVC.Models.DTO
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
