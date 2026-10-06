using System.ComponentModel.DataAnnotations;

namespace asp_02.DTOs
{
    public class AuthorCreateDto
    {
        [Required(ErrorMessage = "Ім'я автора є обов'язковим")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Biography { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }

    }
}
