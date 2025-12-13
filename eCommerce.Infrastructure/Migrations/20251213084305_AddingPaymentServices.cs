using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingPaymentServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51821ec8-e2cb-4540-b7c4-8a4ebe2d2686");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0560166-7057-4452-a9a3-e1222552ec31");

            migrationBuilder.CreateTable(
                name: "CheckoutAchieves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutAchieves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bd8b742-c4da-4b52-9e94-b07cfc57a129", "ce7f546c-0988-4114-a6cd-2c74c7bb9e88", "Admin", "ADMIN" },
                    { "8515e360-5eb5-4290-8b1e-28f8fb280373", "e39dc57d-68d4-4b70-814e-a98e06484230", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("26a92634-f81a-457f-b2d0-2ac4a2545c19"), "Apple Pay" },
                    { new Guid("2e656a11-025c-45be-98ab-a63d701d83bf"), "Credit/Debit Card" },
                    { new Guid("3f2f1ca5-54ff-4da5-a208-e8f185404710"), "Cash on Delivery" },
                    { new Guid("89ce868a-55c7-470a-8486-69b5716f0f63"), "PayPal" },
                    { new Guid("96a773df-c76a-4147-b393-b260886b89fc"), "Vodafone Cash" },
                    { new Guid("b4b3cfec-4930-40b0-97c5-8a57d88d67e3"), "Bank Transfer" },
                    { new Guid("ea34f9f7-c202-4ee2-85c1-1d7f1c80c831"), "Google Pay" },
                    { new Guid("ec213943-0240-4a05-a6d5-200f899c45c0"), "Fawry" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutAchieves");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bd8b742-c4da-4b52-9e94-b07cfc57a129");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8515e360-5eb5-4290-8b1e-28f8fb280373");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "51821ec8-e2cb-4540-b7c4-8a4ebe2d2686", "f7483d71-3f7d-4f7a-9dc9-620783f0f2ed", "User", "USER" },
                    { "c0560166-7057-4452-a9a3-e1222552ec31", "fde65bf4-3e48-493c-937a-fdb4770c2263", "Admin", "ADMIN" }
                });
        }
    }
}
