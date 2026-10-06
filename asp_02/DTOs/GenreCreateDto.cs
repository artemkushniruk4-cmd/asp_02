using System.ComponentModel.DataAnnotations;

namespace asp_02.DTOs
{
    public class GenreCreateDto
    {
        [Required(ErrorMessage = "Назва жанру є обов'язковою")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назва жанру повинна бути від 3 до 50 символів")]
        public string Name { get; set; } = string.Empty;
    }
}
