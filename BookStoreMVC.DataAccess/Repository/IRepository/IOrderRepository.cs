using BookStoreMVC.Models;
using static BookStoreMVC.Utility.Constants;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public void Update(Order order);
        public void UpdateStatus(Order order, OrderStatus orderStatus);
    }
}
