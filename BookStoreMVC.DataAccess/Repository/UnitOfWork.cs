using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;

namespace BookStoreMVC.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICartItemRepository CartItem { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderItemRepository OrderItem { get; private set; }
        public ICartRepository ShoppingCart { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            CartItem = new CartItemRepository(_db);
            Order = new OrderRepository(_db);
            OrderItem = new OrderItemRepository(_db);
            ShoppingCart = new CartRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
