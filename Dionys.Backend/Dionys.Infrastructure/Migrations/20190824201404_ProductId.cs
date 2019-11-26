using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dionys.Infrastructure.Migrations
{
    public partial class ProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumedProducts_Products_ProductId",
                table: "ConsumedProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ConsumedProducts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumedProducts_Products_ProductId",
                table: "ConsumedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumedProducts_Products_ProductId",
                table: "ConsumedProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ConsumedProducts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumedProducts_Products_ProductId",
                table: "ConsumedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
