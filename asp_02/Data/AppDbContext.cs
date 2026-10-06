using asp_02.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Додаємо 3 категорії на вибір
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Електроніка" },
                new Category { Id = 2, Name = "Побут" },
                new Category { Id = 3, Name = "Аксесуари" }
            );

            // 2. Додаємо по 5 товарів для кожної категорії
            modelBuilder.Entity<Product>().HasData(
                // Категорія 1
                new Product { Id = 1, CategoryId = 1, Name = "Смартфон Alpha", Description = "Дисплей 120 Гц, потрійна камера.", Price = 18500.00m, Amount = 15, Image = "https://picsum.photos" },
                new Product { Id = 2, CategoryId = 1, Name = "Ноутбук MateBook", Description = "Тонкий металевий корпус, 16 ГБ ОЗП.", Price = 34000.00m, Amount = 8, Image = "https://picsum.photos" },
                new Product { Id = 3, CategoryId = 1, Name = "Бездротові Навушники", Description = "Активне шумозаглушення.", Price = 4200.00m, Amount = 25, Image = "https://picsum.photos" },
                new Product { Id = 4, CategoryId = 1, Name = "Смарт-годинник Active", Description = "Моніторинг пульсу та крокомір.", Price = 5900.00m, Amount = 12, Image = "https://picsum.photos" },
                new Product { Id = 5, CategoryId = 1, Name = "Планшет Pro 11", Description = "Яскравий екран для малювання.", Price = 21000.00m, Amount = 7, Image = "https://picsum.photos" },

                // Категорія 2
                new Product { Id = 6, CategoryId = 2, Name = "Кавомашина Еспресо", Description = "Автоматичне приготування капучино.", Price = 16800.00m, Amount = 5, Image = "https://picsum.photos" },
                new Product { Id = 7, CategoryId = 2, Name = "Робот-пилосос", Description = "Сухе та вологе прибирання.", Price = 11500.00m, Amount = 9, Image = "https://picsum.photos" },
                new Product { Id = 8, CategoryId = 2, Name = "Електрочайник Glass", Description = "Швидке закипання, корпус із скла.", Price = 1350.00m, Amount = 30, Image = "https://picsum.photos" },
                new Product { Id = 9, CategoryId = 2, Name = "Мультиварка-Скороварка", Description = "24 автоматичні програми.", Price = 3900.00m, Amount = 14, Image = "https://picsum.photos" },
                new Product { Id = 10, CategoryId = 2, Name = "Блендер Фітнес", Description = "Ідеально для смузі.", Price = 2100.00m, Amount = 20, Image = "https://picsum.photos" },

                // Категорія 3
                new Product { Id = 11, CategoryId = 3, Name = "Шкіряний Гаманець", Description = "Натуральна шкіра.", Price = 950.00m, Amount = 45, Image = "https://picsum.photos" },
                new Product { Id = 12, CategoryId = 3, Name = "Рюкзак Міський", Description = "Водовідштовхувальна тканина.", Price = 1850.00m, Amount = 18, Image = "https://picsum.photos" },
                new Product { Id = 13, CategoryId = 3, Name = "Павербанк 20k mAh", Description = "Швидка зарядка трьох пристроїв.", Price = 1600.00m, Amount = 50, Image = "https://picsum.photos" },
                new Product { Id = 14, CategoryId = 3, Name = "Парасолька-Автомат", Description = "Міцний каркас антивітер.", Price = 750.00m, Amount = 22, Image = "https://picsum.photos" },
                new Product { Id = 15, CategoryId = 3, Name = "Сонцезахисні Окуляри", Description = "Поляризаційні лінзи.", Price = 1200.00m, Amount = 11, Image = "https://picsum.photos" }
            );
        }
    }
}
