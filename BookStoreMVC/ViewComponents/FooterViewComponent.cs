using BookStoreMVC.DataAccess.Repository.IRepository;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        IUnitOfWork _unitOfWork;

        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        public FooterViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
