using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        IUnitOfWork _unitOfWork;

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        public MenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
