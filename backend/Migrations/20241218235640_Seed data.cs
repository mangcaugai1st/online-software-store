using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, "testest", "Giải trí", "giai_tri" },
                    { 2, "testest", "Làm việc", "lam_viec" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "IsAdmin", "Password", "Phone", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8735), "admin@example.com", true, true, "AdminPassword", "0123456789", new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8736), "admin" },
                    { 2, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8740), "user@example.com", true, false, "UserPassword", "0123456789", new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8740), "user" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImagePath", "IsActive", "Name", "Price", "Slug", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8692), "photoshop", "https://logos-world.net/wp-content/uploads/2020/11/Adobe-Photoshop-Logo-2015-2019.png", true, "Photoshop", 1000000m, "photoshop", 100, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8695) },
                    { 2, 1, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8708), "Dota 2", "https://cdn-icons-png.flaticon.com/512/588/588308.png", true, "Dota 2", 100000m, "dota2", 100, new DateTime(2024, 12, 18, 23, 56, 39, 559, DateTimeKind.Utc).AddTicks(8708) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
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
        }
    }
}
