using BookStoreMVC.Models;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        public Product? GetById(int id);
    }
}
