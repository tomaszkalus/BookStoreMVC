using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMVC.DataAccess.Repository
{
    public class CartRepository : Repository<ShoppingCartItem>, ICartRepository
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Cart GetCart(string userId)
        {
            IEnumerable<ShoppingCartItem> items = _db.UserProductShoppingCarts.Where(u => u.userId == userId)
                .Include(u => u.Product)
                .Where(u => u.userId == userId);

            Cart cart = new Cart()
            {
                Items = items,
                UserId = userId
            };
            return cart;
        }

        public void ClearCart(string userId)
        {
            IEnumerable<ShoppingCartItem> items = _db.UserProductShoppingCarts.Where(u => u.userId == userId);
            _db.UserProductShoppingCarts.RemoveRange(items);
        }
    }
}
