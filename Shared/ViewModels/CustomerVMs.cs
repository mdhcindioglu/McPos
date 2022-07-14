using McPos.Shared.Validations.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McPos.Shared.ViewModels
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name:")]
        public string? FullName { get; set; }

        public string? Image { get; set; }

        [Display(Name = "Phone Number:")]
        public string? PhoneNumber { get; set; }
        
        [Display(Name = "Address:")]
        public string? Address1 { get; set; }
        
        public string? Address2 { get; set; }
        
        [Display(Name = "County:")]
        public string? County { get; set; }
        
        [Display(Name = "City:")]
        public string? City { get; set; }

        [Display(Name = "Country:")]
        public string? Country { get; set; }

        //public List<OrderVM>? Orders { get; set; }
    }
}
