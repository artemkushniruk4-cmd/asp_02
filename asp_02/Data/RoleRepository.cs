using asp_02.Models;

namespace asp_02.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role? GetRoleById(int id)
        {
            return _context.Roles.Find(id);
        }

        public Role? GetRoleByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
