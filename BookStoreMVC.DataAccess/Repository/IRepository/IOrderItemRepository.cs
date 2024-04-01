using BookStoreMVC.Models;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        void Update(Product product);
        public Product? GetById(int id);
    }
}
