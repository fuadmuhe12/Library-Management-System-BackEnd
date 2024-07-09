using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_Management_System_BackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24070a6d-def8-459a-bea1-3ba754980e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce0afd5a-d850-476e-a4ec-c80581eb65f7");

            migrationBuilder.AlterColumn<string>(
                name: "Roles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abcb6ee0-3612-4351-bef0-d3120ca695e3", null, "User", "USER" },
                    { "be6365a8-af16-4309-a25d-1822d7fa65dd", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abcb6ee0-3612-4351-bef0-d3120ca695e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be6365a8-af16-4309-a25d-1822d7fa65dd");

            migrationBuilder.AlterColumn<int>(
                name: "Roles",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24070a6d-def8-459a-bea1-3ba754980e5d", null, "Admin", "ADMIN" },
                    { "ce0afd5a-d850-476e-a4ec-c80581eb65f7", null, "User", "USER" }
                });
        }
    }
}
