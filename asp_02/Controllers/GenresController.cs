using asp_02.Data;
using asp_02.DTOs;
using asp_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_02.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/genres (GetAll)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var genres = await _context.Genres.ToListAsync();
            var genresDto = genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            });

            return Ok(genresDto);
        }

        // 2. POST: api/genres (Create)
        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] GenreCreateDto genreCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = new Genre
            {
                Name = genreCreateDto.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            var genreDto = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return CreatedAtAction(nameof(GetAll), new { id = genreDto.Id }, genreDto);
        }

        // 3. PUT: api/genres/{id} (Update)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreCreateDto genreUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound(new { message = $"Жанр з ID {id} не знайдено." });
            }

            genre.Name = genreUpdateDto.Name;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 4. DELETE: api/genres/{id} (Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound(new { message = $"Жанр з ID {id} не знайдено." });
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
