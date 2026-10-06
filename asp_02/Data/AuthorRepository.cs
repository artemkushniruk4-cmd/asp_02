using asp_02.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_02.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author? GetAuthorById(int authorId)
        {
            return _context.Authors
                           .Include(a => a.Products)
                           .FirstOrDefault(a => a.Id == authorId);
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
        }

        public void UpdateAuthor(Author author)
        {
            _context.Authors.Update(author);
        }

        public void DeleteAuthor(int authorId)
        {
            var author = _context.Authors.Find(authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
