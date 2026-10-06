using System.ComponentModel.DataAnnotations;

namespace asp_02.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я автора є обов'язковим")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Biography { get; set; } = string.Empty;

        // Зв'язок «один до багатьох» (в одного автора може бути багато товарів/книг)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
