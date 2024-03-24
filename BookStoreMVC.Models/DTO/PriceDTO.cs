namespace BookStoreMVC.Models.DTO
{
    public class PriceDTO
    {
        public double Raw { get; }
        public string Formatted { get => Raw.ToString("c"); }

        public PriceDTO(double raw)
        {
            Raw = raw;
        }
    }
}
