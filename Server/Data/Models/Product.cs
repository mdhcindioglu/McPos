using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace McPos.Server.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public Group? Group { get; set; }

        public string? Name { get; set; }
        
        public string? Image { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? CostPrice { get; set; }
        
        [Column(TypeName = "decimal(16,2)")]
        public decimal? SalePrice { get; set; }
        
        public string? Barcode { get; set; }
        
        public List<OrderItem>? OrderItems { get; set; }
    }
}
