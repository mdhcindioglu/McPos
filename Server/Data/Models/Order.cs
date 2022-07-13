using System.ComponentModel.DataAnnotations.Schema;

namespace McPos.Server.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? Discount { get; set; }


        public List<OrderItem>? OrderItems { get; set; }
    }
}
