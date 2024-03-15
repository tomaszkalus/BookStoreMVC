using BookStoreMVC.Models;
using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.DataAccess.Repository
{
    public class UserProductShoppingCartRepository : Repository<ShoppingCartItem>, IUserProductShoppingCart
    {
        private ApplicationDbContext _db;

        public UserProductShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public ShoppingCartItem? GetById(int id)
        {
            return _db.UserProductShoppingCarts.Find(id);
        }

        public IEnumerable<ShoppingCartItem> GetByUserId(string userId)
        {
            return _db.UserProductShoppingCarts.Where(u => u.userId == userId).Include("Product");
        }

        public int GetShoppingCartProductsAmount(string userId)
        {
            return _db.UserProductShoppingCarts.Where(u => u.userId == userId).ToList().Count;
        }

        public IEnumerable<Product> GetUserProducts(string userId)
        {
            return _db.UserProductShoppingCarts.Where(u => u.userId == userId).Select(p => p.Product);
        }

        public void Update(ShoppingCartItem userProductShoppingCart)
        {
            _db.UserProductShoppingCarts.Update(userProductShoppingCart);
        }

    }
}
