using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seed_data_for_product_and_product_image_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsActive", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(502), "photoshop", true, "Photoshop", 0m, 100, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(508) },
                    { 2, 1, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(607), "Dota 2", true, "Dota 2", 0m, 100, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(608) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedAt", "ImagePath", "IsActive", "ProductId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(655), "https://logos-world.net/wp-content/uploads/2020/11/Adobe-Photoshop-Logo-2015-2019.png", true, 1 },
                    { 2, new DateTime(2024, 12, 4, 11, 53, 15, 706, DateTimeKind.Utc).AddTicks(658), "https://cdn-icons-png.flaticon.com/512/588/588308.png", true, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
