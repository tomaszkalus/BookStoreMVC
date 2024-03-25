namespace BookStoreMVC.Models.DTO
{
    public class PriceDTO
    {
        public decimal Raw { get; }
        public string Formatted { get => Raw.ToString("c"); }

        public PriceDTO(decimal raw)
        {
            Raw = raw;
        }
    }
}
