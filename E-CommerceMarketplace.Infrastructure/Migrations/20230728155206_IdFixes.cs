using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class IdFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_UserId",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vendors",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_UserId",
                table: "Vendors",
                newName: "IX_Vendors_User_Id");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "Products",
                newName: "Vendor_Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                newName: "IX_Products_Vendor_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_Category_Id");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Orders",
                newName: "Status_Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Orders",
                newName: "Payment_Id");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Orders",
                newName: "Buyer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                newName: "IX_Orders_Status_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                newName: "IX_Orders_Payment_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                newName: "IX_Orders_Buyer_Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63caaf37-dd32-4064-a1a9-3fee67c365d9", "AQAAAAEAACcQAAAAEOWuhTc1tgbFy7HFe6Yoy7kui8qoqMRpLzxNxJZq2fjIj2Q9r0PKYVwgwq8yoc1aAg==", "51f55ba0-d35e-4804-96ed-acadd4c336e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc15fcfe-9148-43bc-abae-a8770193d525", "AQAAAAEAACcQAAAAEH+zR1oHSOW1YbdbSlR2l6mTdN4v2PR0006kJeZmoYswnxsye2m93W/vuHW/eO9eQw==", "5a870683-e982-468a-b253-89dfe74a6ac7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4a12032-39b4-4c7f-b74d-c940298e9aa5", "AQAAAAEAACcQAAAAEJW5NBrUhAQyynrTC0hetuggIBs1UPoYdN2PNCzG/7req47heXlwIOXnxutyND6P8w==", "4b944dba-663d-4efb-ad4d-93a6772bd709" });

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
                name: "FK_Orders_Statuses_Status_Id",
                table: "Orders",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_Vendor_Id",
                table: "Products",
                column: "Vendor_Id",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_User_Id",
                table: "Vendors",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Buyer_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_Payment_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_Status_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_Vendor_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_AspNetUsers_User_Id",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Vendors",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_User_Id",
                table: "Vendors",
                newName: "IX_Vendors_UserId");

            migrationBuilder.RenameColumn(
                name: "Vendor_Id",
                table: "Products",
                newName: "VendorId");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Vendor_Id",
                table: "Products",
                newName: "IX_Products_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "Status_Id",
                table: "Orders",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Payment_Id",
                table: "Orders",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Buyer_Id",
                table: "Orders",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Status_Id",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Payment_Id",
                table: "Orders",
                newName: "IX_Orders_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Buyer_Id",
                table: "Orders",
                newName: "IX_Orders_BuyerId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60718b1c-3d90-4113-86a1-7776b9442951", "AQAAAAEAACcQAAAAENgLEmEG8hOp1XyYFwooVUs0qcCA3DqqLKhv6FEvkNwRX8LfKDqXrpeA8FuyOzCQQw==", "b10d6174-3ee7-4fd0-8ddb-c463e0d5530e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f99a3302-815e-4781-8c53-b9d287de46c4", "AQAAAAEAACcQAAAAENKsRfI+VWJT2drWUp4Zqe6RKTTujSVesmFCxKQjIEc3Di9yGZQyfNdBsxcKx6d9Ng==", "e5682744-8478-408d-b135-da69bb0c328d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf398ff0-a7e2-4446-96c3-8c7f9953acee", "AQAAAAEAACcQAAAAEMxYTuIvmQz5+dtvn/USeIQ8bWc6QWGMaabIhOcHBgER4+FEzBdT0BJKzHme/CvfvA==", "2c1bd8b7-2918-4734-bfb1-5757629208ca" });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId",
                table: "Orders",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_AspNetUsers_UserId",
                table: "Vendors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
