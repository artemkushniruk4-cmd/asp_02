using asp_02.Data;
using asp_02.Services;
using asp_02.DTOs;
using asp_02.Middlewares; // Підключаємо папку з нашим Middleware
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog; // Підключаємо Serilog

var builder = WebApplication.CreateBuilder(args);

// --- НАЛАШТУВАННЯ SERILOG ДЛЯ ЛОГУВАННЯ У ФАЙЛ ---
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Дублювати логи в консоль Visual Studio
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // Запис у файл, новий файл щодня
    .CreateLogger();

// Кажемо додатку використовувати саме Serilog замість стандартного логера
builder.Host.UseSerilog();

// Реєстрація репозиторіїв та сервісів в DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Реєстрація FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<AuthorCreateDtoValidator>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<asp_02.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- ПІДКЛЮЧЕННЯ НАШОГО КАСТОМНОГО MIDDLEWARE ---
// Важливо: ставимо його на самий початок конвеєра, щоб засікати абсолютно весь час
app.UseMiddleware<RequestLoggingMiddleware>();

// Виклик сідера для автоматичного заповнення ролей admin/user в БД
DbSeeder.SeedRoles(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    Log.Information("Додаток успішно запускається...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Додаток завершив роботу некоректно!");
}
finally
{
    Log.CloseAndFlush();
}
