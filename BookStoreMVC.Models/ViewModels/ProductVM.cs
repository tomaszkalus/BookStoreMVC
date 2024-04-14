using BookStoreMVC.Models.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreMVC.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        [MaxFileSizeKb(500)]
        [AllowedExtension(new string[] { ".jpg", "jpeg" })]
        [ImageDimensions(390, 595)]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
