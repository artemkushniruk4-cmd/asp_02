using asp_02.Models;

namespace asp_02.Data
{
    public static class DbSeeder
    {
        public static void SeedRoles(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Перевіряємо, чи таблиця ролей порожня
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role { Name = "admin" },
                        new Role { Name = "user" }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
