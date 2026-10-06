using asp_02.DTOs;
using asp_02.Services;
using Microsoft.AspNetCore.Authorization; // Підключаємо систему авторизації .NET
using Microsoft.AspNetCore.Mvc;

namespace asp_02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")] // Обмежуємо доступ до всього контролера тільки для адмінів
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // 1. GET: api/role
        [HttpGet]
        public ActionResult<IEnumerable<RoleDto>> GetAll()
        {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);
        }

        // 2. GET: api/role/{id}
        [HttpGet("{id}")]
        public ActionResult<RoleDto> GetById(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound(new { message = $"Роль з ID {id} не знайдено." });
            }
            return Ok(role);
        }

        // 3. POST: api/role
        [HttpPost]
        public ActionResult<RoleDto> Create([FromBody] RoleCreateDto roleCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRole = _roleService.CreateRole(roleCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRole);
        }

        // 4. PUT: api/role/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RoleCreateDto roleUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _roleService.UpdateRole(id, roleUpdateDto);
            if (!result)
            {
                return NotFound(new { message = $"Роль з ID {id} не знайдено." });
            }

            return NoContent();
        }

        // 5. DELETE: api/role/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _roleService.DeleteRole(id);
            if (!result)
            {
                return NotFound(new { message = $"Роль з ID {id} не знайдено." });
            }

            return NoContent();
        }
    }
}
