using asp_02.DTOs;

namespace asp_02.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto? GetRoleById(int id);
        RoleDto CreateRole(RoleCreateDto roleCreateDto);
        bool UpdateRole(int id, RoleCreateDto roleUpdateDto);
        bool DeleteRole(int id);
    }
}
