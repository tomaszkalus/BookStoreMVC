using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UserProductShoppingCartRepository : Repository<UserProductShoppingCart>, IUserProductShoppingCart
    {
        private ApplicationDbContext _db;

        public UserProductShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public UserProductShoppingCart? GetById(int id)
        {
            return _db.UserProductShoppingCarts.Find(id);
        }

        public IEnumerable<UserProductShoppingCart> GetByUserId(string userId)
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

        public void Update(UserProductShoppingCart userProductShoppingCart)
        {
            _db.UserProductShoppingCarts.Update(userProductShoppingCart);
        }

    }
}
