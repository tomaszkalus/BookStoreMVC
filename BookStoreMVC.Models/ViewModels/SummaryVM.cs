using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Models.ViewModels
{
    public class SummaryVM
    {
        public Cart Cart { get; set; }
        public ApplicationUser User { get; set; }
    }
}
