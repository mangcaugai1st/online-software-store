using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryslug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "testest", "Giải trí" },
                    { 2, "testest", "Làm việc" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImagePath", "IsActive", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 12, 5, 13, 44, 23, 439, DateTimeKind.Utc).AddTicks(4464), "photoshop", "https://logos-world.net/wp-content/uploads/2020/11/Adobe-Photoshop-Logo-2015-2019.png", true, "Photoshop", 0m, 100, new DateTime(2024, 12, 5, 13, 44, 23, 439, DateTimeKind.Utc).AddTicks(4468) },
                    { 2, 1, new DateTime(2024, 12, 5, 13, 44, 23, 439, DateTimeKind.Utc).AddTicks(4474), "Dota 2", "https://cdn-icons-png.flaticon.com/512/588/588308.png", true, "Dota 2", 0m, 100, new DateTime(2024, 12, 5, 13, 44, 23, 439, DateTimeKind.Utc).AddTicks(4474) }
                });
        }
    }
}
