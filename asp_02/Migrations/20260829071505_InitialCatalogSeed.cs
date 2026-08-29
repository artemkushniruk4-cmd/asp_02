using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace asp_02.Migrations
{
    /// <inheritdoc />
    public partial class InitialCatalogSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Електроніка" },
                    { 2, "Побут" },
                    { 3, "Аксесуари" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 15, 1, "Дисплей 120 Гц, потрійна камера.", "https://picsum.photos", "Смартфон Alpha", 18500.00m },
                    { 2, 8, 1, "Тонкий металевий корпус, 16 ГБ ОЗП.", "https://picsum.photos", "Ноутбук MateBook", 34000.00m },
                    { 3, 25, 1, "Активне шумозаглушення.", "https://picsum.photos", "Бездротові Навушники", 4200.00m },
                    { 4, 12, 1, "Моніторинг пульсу та крокомір.", "https://picsum.photos", "Смарт-годинник Active", 5900.00m },
                    { 5, 7, 1, "Яскравий екран для малювання.", "https://picsum.photos", "Планшет Pro 11", 21000.00m },
                    { 6, 5, 2, "Автоматичне приготування капучино.", "https://picsum.photos", "Кавомашина Еспресо", 16800.00m },
                    { 7, 9, 2, "Сухе та вологе прибирання.", "https://picsum.photos", "Робот-пилосос", 11500.00m },
                    { 8, 30, 2, "Швидке закипання, корпус із скла.", "https://picsum.photos", "Електрочайник Glass", 1350.00m },
                    { 9, 14, 2, "24 автоматичні програми.", "https://picsum.photos", "Мультиварка-Скороварка", 3900.00m },
                    { 10, 20, 2, "Ідеально для смузі.", "https://picsum.photos", "Блендер Фітнес", 2100.00m },
                    { 11, 45, 3, "Натуральна шкіра.", "https://picsum.photos", "Шкіряний Гаманець", 950.00m },
                    { 12, 18, 3, "Водовідштовхувальна тканина.", "https://picsum.photos", "Рюкзак Міський", 1850.00m },
                    { 13, 50, 3, "Швидка зарядка трьох пристроїв.", "https://picsum.photos", "Павербанк 20k mAh", 1600.00m },
                    { 14, 22, 3, "Міцний каркас антивітер.", "https://picsum.photos", "Парасолька-Автомат", 750.00m },
                    { 15, 11, 3, "Поляризаційні лінзи.", "https://picsum.photos", "Сонцезахисні Окуляри", 1200.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
