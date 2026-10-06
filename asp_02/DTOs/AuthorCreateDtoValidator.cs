using FluentValidation;

namespace asp_02.DTOs
{
    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        public AuthorCreateDtoValidator()
        {
            // Правило для імені автора
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ім'я автора є обов'язковим для заповнення.")
                .MinimumLength(3).WithMessage("Ім'я автора повинно містити щонайменше 3 символи.")
                .MaximumLength(100).WithMessage("Ім'я автора не може перевищувати 100 символів.");

            // Правило для біографії
            RuleFor(x => x.Biography)
                .MaximumLength(500).WithMessage("Біографія не може перевищувати 500 символів.");

            // Валідація завантаженого файлу зображення (перевірка розширення та розміру)
            RuleFor(x => x.ImageFile)
                .Must(file => file == null || file.Length <= 2 * 1024 * 1024)
                .WithMessage("Розмір зображення не повинен перевищувати 2 МБ.")
                .Must(file => file == null || HasValidExtension(file.FileName))
                .WithMessage("Дозволені лише файли з розширенням .jpg, .jpeg, .png або .webp.");
        }

        // Допоміжний метод для перевірки дозволених форматів картинок
        private bool HasValidExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerString();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
            return allowedExtensions.Contains(extension);
        }
    }

    // Невеличке розширення для безпечної роботи з рядками (уникнення помилок регістру)
    public static class StringExtensions
    {
        public static string ToLowerString(this string? str) => str?.ToLower() ?? string.Empty;
    }
}
