using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class ItemEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Sale_Id = table.Column<int>(type: "int", nullable: true),
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Sales_Sale_Id",
                        column: x => x.Sale_Id,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "44a0edd8-029b-4a0d-887f-915ff659369d", "AQAAAAEAACcQAAAAEN7coVsV1FKbaC9066F2550fVbW3lBv1gP2SesAewcjFaI0DvC1iOFJKkOi8WgGn9Q==", "0edab6b0-a03b-4ce1-bc02-8c7c77f24a3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "034b38ba-ce54-4022-bc1b-246f78f49a14", "AQAAAAEAACcQAAAAENXO0u0qRg9zy3V7Dg2SDY/YCamP2rZLupQdkLXsrZmnqaJnRflNfq4WSMv7TKf2IQ==", "47f0732c-c7bd-46a6-b51a-a2152f0b0ec3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7760c9fd-22e7-4f7c-8ca2-ba27fe73f81f", "AQAAAAEAACcQAAAAEF1K12movW6DtG1NK3GYNj3TGMQMpyE+hGSQnshmZ+a1INXpNLKrvFBBtne8hDwSnQ==", "990d67d7-5a09-4d34-8fee-aa12b4c57323" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Order_Id",
                table: "Items",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Product_Id",
                table: "Items",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Sale_Id",
                table: "Items",
                column: "Sale_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

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
        }
    }
}
