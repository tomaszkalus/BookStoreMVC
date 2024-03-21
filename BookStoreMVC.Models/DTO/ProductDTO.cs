using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Range(1, 1000, ErrorMessage="The quantity has to be between 1 and 1000")]
        public int Quantity { get; set; }
    }
}
