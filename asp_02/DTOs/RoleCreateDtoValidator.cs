using asp_02.Data;
using FluentValidation;

namespace asp_02.DTOs
{
    public class RoleCreateDtoValidator : AbstractValidator<RoleCreateDto>
    {
        private readonly AppDbContext _context;

        public RoleCreateDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Назва ролі є обов'язковою для заповнення.")
                .MinimumLength(3).WithMessage("Назва ролі повинна містити щонайменше 3 символи.")
                .MaximumLength(50).WithMessage("Назва ролі не може перевищувати 50 символів.")
                .Must(BeUniqueName).WithMessage("Роль з такою назвою вже існує в системі.");
        }

        private bool BeUniqueName(string name)
        {
            // Перевіряємо унікальність без урахування регістру символів
            return !_context.Roles.Any(r => r.Name.ToLower() == name.ToLower());
        }
    }
}
