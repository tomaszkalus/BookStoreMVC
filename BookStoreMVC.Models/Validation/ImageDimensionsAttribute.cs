using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using SixLabors.ImageSharp;
namespace BookStoreMVC.Models.Validation
{
    internal class ImageDimensionsAttribute : ValidationAttribute
    {
        private readonly int _width;
        private readonly int _height;

        public ImageDimensionsAttribute(int width, int height)
        {
            _width = width;
            _height = height;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                using var img = Image.Load(file.OpenReadStream());
                if (img.Width != _width || img.Height != _height)
                {
                    return new ValidationResult($"The image dimensions must be {_width}x{_height} pixels.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
