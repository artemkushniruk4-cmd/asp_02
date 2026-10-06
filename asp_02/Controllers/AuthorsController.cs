using asp_02.DTOs;
using asp_02.Services;
using Microsoft.AspNetCore.Mvc;

namespace asp_02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // 1. GET: api/authors (GetAll)
        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> GetAll()
        {
            var authors = _authorService.GetAllAuthors();
            return Ok(authors);
        }

        // 2. GET: api/authors/{id} (GetById)
        [HttpGet("{id}")]
        public ActionResult<AuthorDto> GetById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { message = $"Автора з ID {id} не знайдено." });
            }
            return Ok(author);
        }

        // 3. GET: api/authors/{id}/books (GetBooksByAuthor)
        [HttpGet("{id}/books")]
        public ActionResult<IEnumerable<BookDto>> GetBooksByAuthor(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new { message = $"Автора з ID {id} не знайдено." });
            }

            var books = _authorService.GetBooksByAuthorId(id);
            return Ok(books);
        }

        // 4. POST: api/authors (Create)
        [HttpPost]
        public ActionResult<AuthorDto> Create([FromBody] AuthorCreateDto authorCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAuthor = _authorService.CreateAuthor(authorCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
        }

        // 5. PUT: api/authors/{id} (Update)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorCreateDto authorUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _authorService.UpdateAuthor(id, authorUpdateDto);
            if (!result)
            {
                return NotFound(new { message = $"Автора з ID {id} не знайдено." });
            }

            return NoContent();
        }

        // 6. DELETE: api/authors/{id} (Delete)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _authorService.DeleteAuthor(id);
            if (!result)
            {
                return NotFound(new { message = $"Автора з ID {id} не знайдено." });
            }

            return NoContent();
        }
    }
}
