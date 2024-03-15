using BookStoreMVC.Models;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        public Category? GetById(int id);
    }
}
