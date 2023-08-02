using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class AddUserToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_User_Id",
                table: "Orders",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_User_Id",
                table: "Orders",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_User_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_User_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Orders");

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
        }
    }
}
