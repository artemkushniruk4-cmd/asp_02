using asp_02.Data;
using asp_02.DTOs;
using asp_02.Models;

namespace asp_02.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment; // Потрібно для роботи з wwwroot

        public AuthorService(IAuthorRepository authorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _authorRepository = authorRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            return authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                ImagePath = a.ImagePath
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
                Biography = author.Biography,
                ImagePath = author.ImagePath
            };
        }

        public AuthorDto CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            string? savedFileName = UploadFile(authorCreateDto.ImageFile);

            var author = new Author
            {
                Name = authorCreateDto.Name,
                Biography = authorCreateDto.Biography,
                ImagePath = savedFileName
            };

            _authorRepository.AddAuthor(author);
            _authorRepository.Save();

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
                ImagePath = author.ImagePath
            };
        }

        public bool UpdateAuthor(int id, AuthorCreateDto authorUpdateDto)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return false;

            author.Name = authorUpdateDto.Name;
            author.Biography = authorUpdateDto.Biography;

            // Якщо завантажено нове фото
            if (authorUpdateDto.ImageFile != null)
            {
                // Видаляємо старе фото, якщо воно існувало
                DeleteFile(author.ImagePath);
                // Зберігаємо нове фото
                author.ImagePath = UploadFile(authorUpdateDto.ImageFile);
            }

            _authorRepository.UpdateAuthor(author);
            _authorRepository.Save();

            return true;
        }

        public bool DeleteAuthor(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) return false;

            // Видаляємо файл фото з диска
            DeleteFile(author.ImagePath);

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

        // --- Вспоміжні приватні методи для роботи з файлами ---
        private string? UploadFile(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            // Створюємо унікальне ім'я файлу (щоб уникнути збігів)
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "authors");

            // Перевіряємо, чи існує папка, якщо ні — створюємо
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return "/images/authors/" + uniqueFileName;
        }

        private void DeleteFile(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return;

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

