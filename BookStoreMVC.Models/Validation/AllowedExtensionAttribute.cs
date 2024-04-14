using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.Validation
{
    internal class AllowedExtensionAttribute : ValidationAttribute
    {
        private string[] _extensions;

        public AllowedExtensionAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"We only accept pictures with the these file extensions: {String.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
