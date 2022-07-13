using System.ComponentModel.DataAnnotations.Schema;

namespace McPos.Shared.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public CustomerVM? Customer { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? Discount { get; set; }

        //public List<OrderItemVM>? OrderItems { get; set; }
    }
}