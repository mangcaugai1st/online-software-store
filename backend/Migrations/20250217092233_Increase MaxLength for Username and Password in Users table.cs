using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseMaxLengthforUsernameandPasswordinUserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8695), new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8698) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8741), new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8742) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8746), new DateTime(2025, 2, 17, 9, 22, 29, 201, DateTimeKind.Utc).AddTicks(8746) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(1962), new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(1965) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(1974), new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(2001), new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(2002) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(2006), new DateTime(2025, 1, 8, 14, 25, 55, 766, DateTimeKind.Utc).AddTicks(2006) });
        }
    }
}
