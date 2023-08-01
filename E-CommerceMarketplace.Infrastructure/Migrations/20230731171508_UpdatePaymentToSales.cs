using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class UpdatePaymentToSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Buyer_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_Payment_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_Buyer_Id",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Products_Buyer_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Buyer_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Buyer_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Buyer_Id",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Payment_Id",
                table: "Orders",
                newName: "Sale_Id");

            migrationBuilder.RenameColumn(
                name: "DatePaid",
                table: "Orders",
                newName: "DateCompleted");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Payment_Id",
                table: "Orders",
                newName: "IX_Orders_Sale_Id");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Buyer_Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_AspNetUsers_Buyer_Id",
                        column: x => x.Buyer_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => new { x.ProductId, x.SaleId });
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7068c393-f9d9-4837-bbca-44cda1c2b0a9", "AQAAAAEAACcQAAAAEIo91B+jdX9a3rJbuVTCiTwVPhLo6lUgmQyCaqANm6zNoi6TZ0DKSTpUkC362zV1ow==", "0ef8aade-86a0-40c8-ba1d-b12a064ea31c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbb2a942-5b7b-4592-b940-8abec2f72029", "AQAAAAEAACcQAAAAEKhFwwdIUYBbgxebt5/yd1/uqjUjYI++sm9R5n9LiPWOT/ESRvtXSjBJG/OLWwDNdQ==", "03efe193-5b7d-42b6-ba30-a458368be48a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50920181-a82d-4645-9469-16820c7e6958", "AQAAAAEAACcQAAAAEHyQUtLyPzp/6+J/ZubVptWQ/n9T3ngJECC8MqvNqEkTODKXPWQI3zvO5CjinrQ9/w==", "3a00ab49-39e1-4eed-8823-fdb28ef12485" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_SaleId",
                table: "ProductSales",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Buyer_Id",
                table: "Sales",
                column: "Buyer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sales_Sale_Id",
                table: "Orders",
                column: "Sale_Id",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sales_Sale_Id",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.RenameColumn(
                name: "Sale_Id",
                table: "Orders",
                newName: "Payment_Id");

            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "Orders",
                newName: "DatePaid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Sale_Id",
                table: "Orders",
                newName: "IX_Orders_Payment_Id");

            migrationBuilder.AddColumn<string>(
                name: "Buyer_Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Buyer_Id",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c896cfc-4a20-4eba-b7ea-bf7abecdc708", "AQAAAAEAACcQAAAAEHrzl25Z0S1LhNhwSE0CPB4A0H/bUJro72e4YJbyv2v9PEuY4NoRtkHx891Jvt3nKg==", "a266e6cb-fb5e-4cb9-b01b-e0472973e23d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01eb9809-27bb-46e4-a051-3ad2133395d6", "AQAAAAEAACcQAAAAEMASAstjF/L9BLWxsyotY3aNtke9kbsoyKpMi9/u0RuxyID09kMZH5ZvXfuRxpK/kQ==", "76334916-6a5b-43c3-848f-0746ce004ea1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f957c9e-87ef-4139-963e-a9af2ef19689", "AQAAAAEAACcQAAAAEJZ+oi+lCKD38bMkpiYoJ91C4VdjMznWHBTC9/YvFMme6x/1dZAA8Z2VjLQ9k021bQ==", "5c245539-c479-4238-9b37-a5379b3183c6" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Buyer_Id",
                table: "Products",
                column: "Buyer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Buyer_Id",
                table: "Orders",
                column: "Buyer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Buyer_Id",
                table: "Orders",
                column: "Buyer_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_Payment_Id",
                table: "Orders",
                column: "Payment_Id",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_Buyer_Id",
                table: "Products",
                column: "Buyer_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
