using asp_02.Data;
using asp_02.DTOs;
using asp_02.Models;

namespace asp_02.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography
            }).ToList();
        }

        public AuthorDto? GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography
            };
        }

        public AuthorDto CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            var author = new Author
            {
                Name = authorCreateDto.Name,
                Biography = authorCreateDto.Biography
            };

            _authorRepository.AddAuthor(author);
            _authorRepository.Save();

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography
            };
        }

        public bool UpdateAuthor(int id, AuthorCreateDto authorUpdateDto)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return false;

            author.Name = authorUpdateDto.Name;
            author.Biography = authorUpdateDto.Biography;

            _authorRepository.UpdateAuthor(author);
            _authorRepository.Save();

            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return false;

            _authorRepository.DeleteAuthor(id);
            _authorRepository.Save();

            return true;
        }
        public IEnumerable<BookDto> GetBooksByAuthorId(int authorId)
        {
            var books = _authorRepository.GetBooksByAuthorId(authorId);
            return books.Select(b => new BookDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                Price = b.Price
            }).ToList();
        }


    }
}
