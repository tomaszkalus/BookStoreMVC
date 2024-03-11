using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Models.JsonModels
{
    public class NewCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
