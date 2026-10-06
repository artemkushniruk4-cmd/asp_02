using asp_02.Models;

namespace asp_02.Data
{
    public interface IUserRepository
    {
        User? GetUserById(int id);
        void UpdateUser(User user);
        void Save();
    }
}
