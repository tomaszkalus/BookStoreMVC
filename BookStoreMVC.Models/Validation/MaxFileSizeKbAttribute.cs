using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.Validation
{
    internal class MaxFileSizeKbAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeKbAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {

                if (file.Length > (1024 * _maxFileSize))
                {
                    return new ValidationResult($"The maximum allowed file size is {_maxFileSize} KB.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
