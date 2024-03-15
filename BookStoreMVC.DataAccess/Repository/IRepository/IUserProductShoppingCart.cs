using BookStoreMVC.Models;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IUserProductShoppingCart : IRepository<ShoppingCartItem>
    {
        void Update(ShoppingCartItem userProductShoppingCart);
        public ShoppingCartItem? GetById(int id);
        public IEnumerable<ShoppingCartItem> GetByUserId(string userId);
        public IEnumerable<Product> GetUserProducts(string userId);
        public int GetShoppingCartProductsAmount(string userId);
    }
}
