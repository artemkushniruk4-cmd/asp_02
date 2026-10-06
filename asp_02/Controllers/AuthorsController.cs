using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using asp_02.DTOs;
using asp_02.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace asp_02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // 1. ВХІД ТА ГЕНЕРАЦІЯ ТОКЕНУ З РОЛЛЮ
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Шукаємо користувача через сервіс (для простоти припустимо, що сервіс повертає об'єкт користувача з базовою роллю)
            // Якщо у вашій моделі User поки немає ролі, ми тимчасово ставимо "admin" для тестів
            if (loginDto.Email == "admin@gmail.com" && loginDto.Password == "admin123")
            {
                var token = GenerateJwtToken(1, loginDto.Email, "admin");
                return Ok(new { Token = token });
            }

            if (loginDto.Email == "user@gmail.com" && loginDto.Password == "user123")
            {
                var token = GenerateJwtToken(2, loginDto.Email, "user");
                return Ok(new { Token = token });
            }

            return Unauthorized(new { message = "Невірний логін або пароль." });
        }

        // 2. МЕТОД ЗМІНИ ПАРОЛЮ (ЯКИЙ МИ ПИСАЛИ РАНІШЕ)
        [Authorize]
        [HttpPost("change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized(new { message = "Відсутній токен." });

            if (!int.TryParse(userIdClaim.Value, out int userId)) return BadRequest();

            var result = _userService.ChangePassword(userId, changePasswordDto);
            if (!result) return BadRequest(new { message = "Помилка зміни паролю." });

            return Ok(new { message = "Пароль успішно змінено!" });
        }

        // --- Приватний метод створення токену (Ось сюди зашивається роль!) ---
        private string GenerateJwtToken(int userId, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                
                // Окремий Клейм для ролі (Адмін або Юзер), який вимагає викладач!
                new Claim(ClaimTypes.Role, role)
            };

            // Секретний ключ береться з налаштувань appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "СуперСекретнийКлючДляЛабораторноїРоботи123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "asp_02_App",
                audience: _configuration["Jwt:Audience"] ?? "asp_02_Users",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
