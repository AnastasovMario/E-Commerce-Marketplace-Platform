using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class MandatoryStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Status_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08001a08-fe25-4413-93e5-33e250230023", "AQAAAAEAACcQAAAAEMNi9pyJxtCd1siOd/noXt9MvuZOJnwbSnLci8OzRVVxX8zYbbfNwUSJsmafNyPrJQ==", "f1a17060-5e55-41d0-804f-34230722b07b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "711d37a1-71cf-4eb8-8caa-bed642b92cf7", "AQAAAAEAACcQAAAAEEFbIb0m6DWDzDhziVvGQA6x7FzvKkGqivTIKycz9MvWuLNvEB9209GhylOM8xoQIg==", "2105d12b-2896-4d75-aa3e-cef7ababf9a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a66e40f-30ae-4988-9406-9f7aeb163133", "AQAAAAEAACcQAAAAEJP5kKDqwiGksm/vpeheo5exEGlOt5GRGjLhtdrbwtENRnKL6o06OH0ayy+f7kVWQA==", "8b584840-fb01-4a16-9ff9-486b3b0e19e4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Status_Id",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e27ec2c-b60a-46ab-b77e-2b502e487cab", "AQAAAAEAACcQAAAAEJmerfr8958JqkTneQYE3Gla1PjSqpfrDpogu/lczLl945hsbGTgK7hhL8cxBQ0Ruw==", "ffbc65e0-48de-499a-9b36-9185b3a4e30d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d9af40f-a668-4cbf-85b5-79d23a85b300", "AQAAAAEAACcQAAAAENxHRxmsYVIXoywSwtAtYlHsCIOiv74gsLgStQGoCgCcxO0+qkiFT9pgMThXcT6llA==", "9a81201b-71b1-43b0-8c0d-3faf57255f15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf9fa067-6195-4c76-898a-a91a047f9c41", "AQAAAAEAACcQAAAAEEM30uGJrVpWV4B/rCIwESlldxMWDcdHZ8R0fbiWHYUiP6iNyYlhSww0ACbtFXm6mg==", "82b4f4fb-afff-4bf0-bc89-4adb95e32276" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Id");
        }
    }
}
