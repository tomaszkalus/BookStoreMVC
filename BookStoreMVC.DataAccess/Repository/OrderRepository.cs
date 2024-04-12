using BookStoreMVC.DataAccess.Data;
using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using BookStoreMVC.Utility;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Order> GetAllUserOrders(string userId)
        {
            var orders = _db.Orders
                .Where(o => o.UserId == userId)
                .ToList();
            foreach (var order in orders)
            {
                order.Items = _db.OrderItems
                    .Where(oi => oi.OrderId == order.Id)
                    .ToList();
            }

            return orders;
        }
    }
}
