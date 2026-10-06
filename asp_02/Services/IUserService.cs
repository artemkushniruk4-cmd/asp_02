using asp_02.DTOs;

namespace asp_02.Services
{
    public interface IUserService
    {
        bool ChangePassword(int userId, ChangePasswordDto changePasswordDto);
    }
}
