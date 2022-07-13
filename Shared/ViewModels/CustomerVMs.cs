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

        [Display(Name = "Full Name:")]
        public string? FullName { get; set; }

        public string? Image { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Contry { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        //public List<OrderVM>? Orders { get; set; }
    }
}
