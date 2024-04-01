namespace BookStoreMVC.Utility
{
    public static class Constants
    {
        public static class Prices
        {
            public const decimal Shipping = 5.0m;
            public const decimal Vat = 0.23m;
        }

        public enum OrderStatus
        {
            Pending,
            Processing,
            Approved,
            Completed,
            Cancelled
        }
    }
}
