using asp_02.Models;

namespace asp_02.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
        void Save();
    }
}
