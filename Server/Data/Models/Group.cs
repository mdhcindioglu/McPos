using System.ComponentModel.DataAnnotations.Schema;

namespace McPos.Server.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Group? Parent { get; set; }

        public string? Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
