using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp_02.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву товару")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Назва має містити від 3 до 100 символів")]
        [Display(Name = "Назва товару")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Додайте опис товару")]
        [StringLength(1000, ErrorMessage = "Опис не може бути довшим за 1000 символів")]
        [Display(Name = "Опис")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Вкажіть ціну")]
        [Range(0.01, 1000000.00, ErrorMessage = "Ціна повинна бути більшою за 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Ціна (грн)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Вкажіть кількість")]
        [Range(0, 10000, ErrorMessage = "Кількість не може бути від'ємною")]
        [Display(Name = "Кількість (шт)")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Додайте посилання на зображення")]
        [Url(ErrorMessage = "Введіть коректне URL-посилання (наприклад, https://...)")]
        [Display(Name = "Посилання на фото")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оберіть категорію")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }

    }
}
