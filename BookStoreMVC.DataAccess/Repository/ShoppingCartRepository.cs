using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCartItem>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Cart GetCart(string userId)
        {
            // Product is not present in the ShoppingCartItem class.
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
    }
}
