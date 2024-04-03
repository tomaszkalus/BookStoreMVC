using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Utility;

namespace BookStoreMVC.DataAccess.Repository
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {

        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            _db.Orders.Update(order);
        }

        public void UpdateStatus(Order order, Constants.OrderStatus orderStatus)
        {
            order.Status = orderStatus;
            _db.Orders.Update(order);
        }
        public Order GetOrder(int orderId)
        {
            return _db.Orders.FirstOrDefault(o => o.Id == orderId);
        }
    }
}
