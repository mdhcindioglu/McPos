using System.ComponentModel.DataAnnotations.Schema;

namespace McPos.Server.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Qty { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? Discount { get; set; }
    }
}
