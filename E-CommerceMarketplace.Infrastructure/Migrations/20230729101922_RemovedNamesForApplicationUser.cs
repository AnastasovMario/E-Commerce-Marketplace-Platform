using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class RemovedNamesForApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Vendors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vendors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ade846ee-df07-45fa-a455-0ca116b853bd", null, null, "AQAAAAEAACcQAAAAENXi2qIqEWlytuGxDpfSt5hsidDeVcXy8fqcVtb1KUzaPajz2+4M26siIfmAk8tSWA==", "e4647b6d-a3bd-4792-be43-5f29be7fe081" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "030e2a9e-4c62-472b-996a-7d5e6175add9", null, null, "AQAAAAEAACcQAAAAEIR/tyiAmOrBAhw/psQv7bAHrmvCHT6olffGYWEWXQbSZLJMACnefyWXKxlHa8PjjQ==", "cec8bdaa-7692-46e9-b74b-67db07ddaa35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64991114-31a8-4373-bb5d-50358babd700", null, null, "AQAAAAEAACcQAAAAEDile4M/RbEY91kPda1khaWVQ7+Z4tbD0BT6ZC0sX2lF8AJDiHoiwIVUkrM8BhwlZQ==", "5c5c12ae-8447-4154-9dc0-d223fbef124a" });

            migrationBuilder.UpdateData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName" },
                values: new object[] { "Linda", "Michaels" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vendors");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63caaf37-dd32-4064-a1a9-3fee67c365d9", "Mario", "Anastasov", "AQAAAAEAACcQAAAAEOWuhTc1tgbFy7HFe6Yoy7kui8qoqMRpLzxNxJZq2fjIj2Q9r0PKYVwgwq8yoc1aAg==", "51f55ba0-d35e-4804-96ed-acadd4c336e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc15fcfe-9148-43bc-abae-a8770193d525", "Guest", "Guestov", "AQAAAAEAACcQAAAAEH+zR1oHSOW1YbdbSlR2l6mTdN4v2PR0006kJeZmoYswnxsye2m93W/vuHW/eO9eQw==", "5a870683-e982-468a-b253-89dfe74a6ac7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4a12032-39b4-4c7f-b74d-c940298e9aa5", "Linda", "Michaels", "AQAAAAEAACcQAAAAEJW5NBrUhAQyynrTC0hetuggIBs1UPoYdN2PNCzG/7req47heXlwIOXnxutyND6P8w==", "4b944dba-663d-4efb-ad4d-93a6772bd709" });
        }
    }
}
