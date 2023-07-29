using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class AddStatusToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Statuses",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Status_Id",
                table: "Products",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status_Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status_Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Status_Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Status_Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Status_Id",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Status_Id",
                table: "Products",
                column: "Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Statuses_Status_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Status_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status_Id",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "852d5796-ac89-4cb9-95ec-207625b197de", "AQAAAAEAACcQAAAAEM/ySMoU+O8MdrAoqFNFU6YxoNumhkIwdvFITHrtBrxNhpAwsAwbsZ9GdUdUYCA+ug==", "b1dba91f-2268-4a5f-996a-d4776e9bbe7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a039af9a-519f-4c9b-91c7-1b90144a26a4", "AQAAAAEAACcQAAAAEP3RV5IjlcH3AkV5j9+2C0mD8+J5Yr1tbp4wWBwif0EKCrF/RL+2V5yFpH2O+JekiQ==", "c1201a1b-931b-4f29-b4af-011733e03118" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e941f8a-0fe8-42ba-90fa-d1999632719a", "AQAAAAEAACcQAAAAEOgp6fABSx46pKCuE7C4kxXt+0JMr0+8v/4TUAqV/3exwBXRK/ckGEt9g2LzAw6Ekg==", "39928b47-8721-48e8-8344-3ff40cc3cd2e" });
        }
    }
}
