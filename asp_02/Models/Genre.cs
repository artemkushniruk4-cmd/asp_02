using System.ComponentModel.DataAnnotations;

namespace asp_02.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва жанру є обов'язковою")]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Зв'язок багатьох до багатьох (у одного жанру може бути багато книг)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
