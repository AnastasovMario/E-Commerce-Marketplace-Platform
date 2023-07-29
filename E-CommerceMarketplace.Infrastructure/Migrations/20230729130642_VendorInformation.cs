using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class VendorInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ade846ee-df07-45fa-a455-0ca116b853bd", "AQAAAAEAACcQAAAAENXi2qIqEWlytuGxDpfSt5hsidDeVcXy8fqcVtb1KUzaPajz2+4M26siIfmAk8tSWA==", "e4647b6d-a3bd-4792-be43-5f29be7fe081" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "030e2a9e-4c62-472b-996a-7d5e6175add9", "AQAAAAEAACcQAAAAEIR/tyiAmOrBAhw/psQv7bAHrmvCHT6olffGYWEWXQbSZLJMACnefyWXKxlHa8PjjQ==", "cec8bdaa-7692-46e9-b74b-67db07ddaa35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64991114-31a8-4373-bb5d-50358babd700", "AQAAAAEAACcQAAAAEDile4M/RbEY91kPda1khaWVQ7+Z4tbD0BT6ZC0sX2lF8AJDiHoiwIVUkrM8BhwlZQ==", "5c5c12ae-8447-4154-9dc0-d223fbef124a" });
        }
    }
}
