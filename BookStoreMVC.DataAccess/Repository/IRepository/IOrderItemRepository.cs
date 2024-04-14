using BookStoreMVC.Models;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        public OrderItem? GetById(int id);
    }
}
