using System.ComponentModel.DataAnnotations;

namespace asp_02.DTOs
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Поточний пароль є обов'язковим")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Новий пароль є обов'язковим")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Новий пароль має бути не менше 6 символів")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
