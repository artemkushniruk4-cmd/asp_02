using asp_02.Data;
using asp_02.DTOs;

namespace asp_02.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _roleRepository.GetAllRoles();
            return roles.Select(r => r.ToDto()).ToList();
        }

        public RoleDto? GetRoleById(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            return role?.ToDto();
        }

        public RoleDto CreateRole(RoleCreateDto roleCreateDto)
        {
            var role = roleCreateDto.ToEntity();
            _roleRepository.AddRole(role);
            _roleRepository.Save();
            return role.ToDto();
        }

        public bool UpdateRole(int id, RoleCreateDto roleUpdateDto)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return false;

            role.Name = roleUpdateDto.Name;
            _roleRepository.UpdateRole(role);
            _roleRepository.Save();
            return true;
        }

        public bool DeleteRole(int id)
        {
            var role = _roleRepository.GetRoleById(id);
            if (role == null) return false;

            _roleRepository.DeleteRole(role);
            _roleRepository.Save();
            return true;
        }
    }
}
