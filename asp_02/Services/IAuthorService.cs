using asp_02.DTOs;

namespace asp_02.Services
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        IEnumerable<BookDto> GetBooksByAuthorId(int authorId);

        AuthorDto? GetAuthorById(int id);
        AuthorDto CreateAuthor(AuthorCreateDto authorCreateDto);
        bool UpdateAuthor(int id, AuthorCreateDto authorUpdateDto);
        bool DeleteAuthor(int id);
    }
}
