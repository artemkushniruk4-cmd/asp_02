using asp_02.Data;
using asp_02.DTOs;

namespace asp_02.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ChangePassword(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null) return false;

            // Проста перевірка паролю (без хешування, як у вашому поточному рівні ТЗ)
            if (user.Password != changePasswordDto.CurrentPassword)
            {
                return false;
            }

            user.Password = changePasswordDto.NewPassword;

            _userRepository.UpdateUser(user);
            _userRepository.Save();

            return true;
        }
    }
}
