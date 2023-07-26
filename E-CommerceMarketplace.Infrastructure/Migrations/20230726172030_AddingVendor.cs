using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class AddingVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "VendorId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32af8fcd-9acf-45cc-b864-2ff97f9083c1", "AQAAAAEAACcQAAAAEMIb/9veHUnhOK1SUVgaN08HjryaCC+ZBxjjVzVLiC+ZC0OcMwwl7kSgMPqIC6rvCQ==", "77083705-bc1e-47fd-bb0a-11be3b470e1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0781fbb-2dad-47a4-8b7e-18e64ca8e89a", "AQAAAAEAACcQAAAAEAl0MLoJOXP4h4BJ1EjNd5rSN3XbP+1g6xLbP2MhuqFlrbHXCOhbUPn0GXWN/uRldg==", "c1e37c80-dee1-48e5-a96a-378d94d947de" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b4dca2d-dc7b-47b9-94d6-eaf948e36a35", "AQAAAAEAACcQAAAAEDrWITygcHQaM8EqwWAhANkPpOIm+A6SrrXbvQvG2Y8qbRlwWnovKJ8zEjvKR1w2Jw==", "56cf16e4-c7f6-4a7f-86ad-52079f8baa6d" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Clothing & Fashion" },
                    { 3, "Home & Garden" },
                    { 4, "Health & Beauty" },
                    { 5, "Books & Magazines" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_VendorId",
                table: "Products",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Vendors_VendorId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_VendorId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "VendorId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4254895e-31d0-418b-8f37-b5e7c0f794df", "AQAAAAEAACcQAAAAEONh7fee3YP7hzKwbmLsUASbBApaA7zPBJD0/XlqtoPFx4mejXvcEEXKoXTJ76SSGA==", "fef33a18-e4c7-4cf5-bb42-e276654c5af3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4025eff-9bd3-4062-bb18-3df51e256352", "AQAAAAEAACcQAAAAEF3+nYWZY8j3MSjiogn5jC9GqKwlF44sod5Y/UkKphU51LdTzhzWAEnAOqzdGZeGkw==", "0c32fd3f-b56c-4f4a-8632-6f1c703224a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "312536d2-8dfa-4b2b-914e-6b02f6502c6c", "AQAAAAEAACcQAAAAEAx/1Tqw+ry95uuBjSJeZtTDpIYh44aUrxXQZlw/YkGw2iSv5m+gogtY0xq44oSTcw==", "e154d056-f45a-46ac-a302-b9aadece977a" });
        }
    }
}
