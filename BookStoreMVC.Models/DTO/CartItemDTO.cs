namespace BookStoreMVC.Models.DTO
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public PriceDTO UnitPrice { get; set; }
        public PriceDTO TotalPrice { get; set; }
    }
}
