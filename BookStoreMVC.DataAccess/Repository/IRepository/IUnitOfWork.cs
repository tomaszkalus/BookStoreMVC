using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IUserProductShoppingCart UserProductShoppingCart { get; }
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}
