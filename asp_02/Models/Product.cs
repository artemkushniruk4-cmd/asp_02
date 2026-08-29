using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_02.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string Image { get; set; } = string.Empty;

        // Зв'язок з категорією (Foreign Key)
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
