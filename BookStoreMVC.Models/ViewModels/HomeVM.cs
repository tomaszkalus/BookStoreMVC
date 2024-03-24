namespace BookStoreMVC.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
    }
}
