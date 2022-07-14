using System.ComponentModel.DataAnnotations;

namespace McPos.Server.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        public string? Image { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
