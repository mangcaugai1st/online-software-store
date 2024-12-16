using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Increase_the_password_character_limit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3892), new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3904), new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3905) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 15, 20, 57, 382, DateTimeKind.Utc).AddTicks(1745), new DateTime(2024, 12, 5, 15, 20, 57, 382, DateTimeKind.Utc).AddTicks(1748) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 5, 15, 20, 57, 382, DateTimeKind.Utc).AddTicks(1756), new DateTime(2024, 12, 5, 15, 20, 57, 382, DateTimeKind.Utc).AddTicks(1756) });
        }
    }
}
