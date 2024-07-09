using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_Management_System_BackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class categorySeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abcb6ee0-3612-4351-bef0-d3120ca695e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be6365a8-af16-4309-a25d-1822d7fa65dd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01f4397d-a084-49cd-a2c5-43eeb46efc73", null, "User", "USER" },
                    { "f34fdd6d-95d5-466e-9747-9a276faa01b8", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01f4397d-a084-49cd-a2c5-43eeb46efc73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f34fdd6d-95d5-466e-9747-9a276faa01b8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abcb6ee0-3612-4351-bef0-d3120ca695e3", null, "User", "USER" },
                    { "be6365a8-af16-4309-a25d-1822d7fa65dd", null, "Admin", "ADMIN" }
                });
        }
    }
}
