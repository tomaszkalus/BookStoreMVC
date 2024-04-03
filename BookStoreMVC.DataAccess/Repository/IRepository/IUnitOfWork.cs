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
        ICartItemRepository CartItem { get; }
        IOrderRepository Order { get; }
        IOrderItemRepository OrderItem { get; }
        ICartRepository ShoppingCart { get; }

        void Save();
    }
}
