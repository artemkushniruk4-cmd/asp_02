using asp_02.DTOs;
using asp_02.Models;

namespace asp_02.Services
{
    public static class RoleMapper
    {
        public static RoleDto ToDto(this Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToEntity(this RoleCreateDto dto)
        {
            return new Role
            {
                Name = dto.Name
            };
        }
    }
}
