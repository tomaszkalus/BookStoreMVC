using BookStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public int productId { get; set; }
        [Required]
        [Range(1, 1000)]
        public int quantity { get; set; }
        [ForeignKey("userId")]
        public ApplicationUser User { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }
        [NotMapped]
        public decimal TotalPrice
        {
            get { return Product.Price * quantity; }
        }


    }
}
