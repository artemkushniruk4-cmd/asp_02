using System.ComponentModel.DataAnnotations;

namespace asp_02.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Зв'язок: в одній категорії може бути багато товарів
        public List<Product> Products { get; set; } = new();
    }
}
