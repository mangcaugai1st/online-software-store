using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Add_value_of_price_in_Product_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3836), 1000000m, new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3839) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3853), 100000m, new DateTime(2024, 12, 16, 3, 11, 29, 460, DateTimeKind.Utc).AddTicks(3853) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3892), 0m, new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Price", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3904), 0m, new DateTime(2024, 12, 16, 2, 9, 31, 96, DateTimeKind.Utc).AddTicks(3905) });
        }
    }
}
