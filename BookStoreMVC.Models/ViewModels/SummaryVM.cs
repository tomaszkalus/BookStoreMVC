using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookStoreMVC.Models.ViewModels
{
    public class SummaryVM
    {
        public Cart Cart { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool RememberAddress { get; set; }
    }
}
