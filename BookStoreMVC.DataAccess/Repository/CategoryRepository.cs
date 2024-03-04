using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }

        public Category? GetById(int id)
        {
            return _db.Categories.Find(id);
        }
    }
}
