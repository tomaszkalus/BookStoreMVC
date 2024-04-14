using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string? Author { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public decimal Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public decimal Price100 { get; set; }

        public int? CategoryId { get; set; }
        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        private string? imageUrl;
        [ValidateNever]
        public string? ImageUrl
        {
            get => string.IsNullOrEmpty(imageUrl) ? "/images/product/no_photo.jpg" : imageUrl;
            set => imageUrl = value;
        }

        [Display(Name = "New")]
        public bool IsNew { get; set; }
        [Display(Name = "Bestseller")]
        public bool IsBestseller { get; set; }
        [Display(Name = "Special Offer")]
        public bool IsSpecialOffer { get; set; }


    }
}
