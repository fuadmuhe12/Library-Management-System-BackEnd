using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library_Management_System_BackEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateduserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81ac617c-1742-468e-bdcd-f855951026eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb6a4f1f-0f43-4711-938f-f41dc5c245cb");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24070a6d-def8-459a-bea1-3ba754980e5d", null, "Admin", "ADMIN" },
                    { "ce0afd5a-d850-476e-a4ec-c80581eb65f7", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24070a6d-def8-459a-bea1-3ba754980e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce0afd5a-d850-476e-a4ec-c80581eb65f7");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81ac617c-1742-468e-bdcd-f855951026eb", null, "Admin", "ADMIN" },
                    { "bb6a4f1f-0f43-4711-938f-f41dc5c245cb", null, "User", "USER" }
                });
        }
    }
}
