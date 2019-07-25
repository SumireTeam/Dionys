using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dionys.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDTO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Protein = table.Column<float>(nullable: false),
                    Fat = table.Column<float>(nullable: false),
                    Carbohydrates = table.Column<float>(nullable: false),
                    Calories = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Protein = table.Column<float>(nullable: false),
                    Fat = table.Column<float>(nullable: false),
                    Carbohydrates = table.Column<float>(nullable: false),
                    Calories = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumedProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    Weight = table.Column<float>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ProductDTO",
                columns: new[] { "Id", "Calories", "Carbohydrates", "Description", "Fat", "Name", "Protein" },
                values: new object[,]
                {
                    { new Guid("274684a2-d52b-4fb8-8bad-1f065ba76071"), 24f, 4.5f, "Баклажан как баклажан. На вкус как баклажан, на вид как баклажан. Ничего удивительного.", 0.1f, "Баклажан", 1.2f },
                    { new Guid("274684a2-d52b-4fb8-8bad-1f065ba76072"), 556f, 58f, "Вкусная шоколадка. Жаль, что мало. Хотелось бы ещё. Обязательно надо закупать огромными партиями.", 34f, "Alpen Gold. Молочный шоколад. Чернично-йогуртовая начинка, 90 г", 3.9f },
                    { new Guid("274684a2-d52b-4fb8-8bad-1f065ba76073"), 160f, 0f, "Цыплёнок как циплёнок. На вкус был как цыплёнок...", 13f, "Сибирские колбасы. Окорочок цыплёнка-бройлера, 260 г", 10f },
                    { new Guid("274684a2-d52b-4fb8-8bad-1f065ba76074"), 510f, 66f, "Внешний вид напоминает крекеры. На упаковке написано \"крекеры\". Возможно крекеры.", 24f, "Яшкино. Французский крекер с кунжутом, 185 г", 8.5f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumedProducts_ProductId",
                table: "ConsumedProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumedProducts");

            migrationBuilder.DropTable(
                name: "ProductDTO");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
