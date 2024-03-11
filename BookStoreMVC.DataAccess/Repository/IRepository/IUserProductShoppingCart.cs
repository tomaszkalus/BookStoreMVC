using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUserProductShoppingCart : IRepository<UserProductShoppingCart>
    {
        void Update(UserProductShoppingCart userProductShoppingCart);
        public UserProductShoppingCart? GetById(int id);
        public IEnumerable<UserProductShoppingCart> GetByUserId(string userId);
        public IEnumerable<Product> GetUserProducts(string userId);
        public int GetShoppingCartProductsAmount(string userId);
    }
}
