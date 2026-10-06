using asp_02.Models;

namespace asp_02.Data
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role? GetRoleById(int id);
        Role? GetRoleByName(string name); // Вимагається ТЗ тільки в репозиторії
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void Save();
    }
}
