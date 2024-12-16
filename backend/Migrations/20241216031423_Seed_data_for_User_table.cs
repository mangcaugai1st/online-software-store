using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Seed_data_for_User_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(2962), new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(2967) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(2976), new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(2977) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "IsAdmin", "Password", "Phone", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(3036), "admin@example.com", true, true, "AdminPassword", "0123456789", new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(3037), "admin" },
                    { 2, new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(3043), "user@example.com", true, false, "UserPassword", "0123456789", new DateTime(2024, 12, 16, 3, 14, 20, 411, DateTimeKind.Utc).AddTicks(3044), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3836), new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3839) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3853), new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3853) });
        }
    }
}
