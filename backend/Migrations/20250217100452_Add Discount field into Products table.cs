using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountfieldintoProductstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Discount", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5594), 0m, new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5597) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Discount", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5607), 0m, new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5607) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5635) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5639), new DateTime(2025, 2, 17, 10, 4, 52, 253, DateTimeKind.Utc).AddTicks(5639) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Products");

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
    }
}
