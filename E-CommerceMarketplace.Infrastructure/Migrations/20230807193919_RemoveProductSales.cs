using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class RemoveProductSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Sales_Sale_Id",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Items_Sale_Id",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sale_Id",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8921858-b022-485d-a765-32303800b637", "AQAAAAEAACcQAAAAEN4fSn0L+3I/qq9aMSA8RX+F6wxy2WQtrgG99qGKSlj+loVIRrJQnYk3otGpkXJkyA==", "e4580793-1565-45f9-9b7a-a7a89b0c3692" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4df49c88-93a7-4dc4-af09-0509db8d38f5", "AQAAAAEAACcQAAAAEDZHBmQX9P1h+8MuzV2z6iYRUl33zOR3smL0x9FhuCFp/gp4cFfdJToWXisFDlSebg==", "4afd513f-7adb-46cc-82d7-5bb07fa765c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "748e16d5-b706-4c84-8193-cd3d26775530", "AQAAAAEAACcQAAAAEPHL/1BbhqwXLxz4CDudx9zesn+Cls3RkU+XKYxWires2vH3iyLFszreg2jAKF8MSA==", "a8c3718a-39e2-41da-97c0-7e256e1cb896" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sale_Id",
                table: "Items",
                type: "int",
                nullable: true);

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
                values: new object[] { "105f20ff-d64f-43e0-b857-f90e29d8c996", "AQAAAAEAACcQAAAAEGAFStn6ahQCViZMlayk4OFwpcrhVFhSmIFxa5O3jdokcIEfeju6op0FLYOtz4gW9A==", "b6b5284c-f46f-4567-af1b-62cb5f4e096b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31b96e9b-d48a-4e0b-b6f9-008698898aac", "AQAAAAEAACcQAAAAELHn7TfRKiN0PtIirW9QPVOA9V4oaYiKMcoeyIkhXh4/HuRowp6Fj2NYf2oJjnITyA==", "24b27818-ef66-4a7b-b34f-2c7f22445ce7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "754539db-353b-4c1d-928c-b5c48dfd4047", "AQAAAAEAACcQAAAAEMxSicFu80R8RD/nHpcXI6XvYU5DyVY2XIh0cH577jq2mYmUKbjwP84THkjMrHMBew==", "840f504e-034f-496c-bdd2-7389bc1970d1" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Paid" },
                    { 2, "Unpaid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Sale_Id",
                table: "Items",
                column: "Sale_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_SaleId",
                table: "ProductSales",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Sales_Sale_Id",
                table: "Items",
                column: "Sale_Id",
                principalTable: "Sales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
