using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class AddBuyerToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buyer_Id",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_Buyer_Id",
                table: "Products",
                column: "Buyer_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_Buyer_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Buyer_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Buyer_Id",
                table: "Products");

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
        }
    }
}
