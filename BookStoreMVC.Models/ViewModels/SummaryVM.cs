using Microsoft.AspNetCore.Identity;

namespace BookStoreMVC.Models.ViewModels
{
    public class SummaryVM
    {
        public CartVM Cart { get; set; }
        public ApplicationUser User { get; set; }
    }
}
