using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51821ec8-e2cb-4540-b7c4-8a4ebe2d2686", "f7483d71-3f7d-4f7a-9dc9-620783f0f2ed", "User", "USER" },
                    { "c0560166-7057-4452-a9a3-e1222552ec31", "fde65bf4-3e48-493c-937a-fdb4770c2263", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51821ec8-e2cb-4540-b7c4-8a4ebe2d2686");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0560166-7057-4452-a9a3-e1222552ec31");
        }
    }
}
