using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class RemoveStatusFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_Status_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Status_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status_Id",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d84850bf-a432-414c-9361-39c84f53da0c", "AQAAAAEAACcQAAAAEFYiA/7d3VTsVL5781oA0rP/sltgfGaSvNk5VtiRN+fDdscLhNBoZRZtD4JxT7lkdg==", "8aead180-eb54-43d6-811d-4a10c9cdcb23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "330e80fa-894f-47e3-b5e3-7ebf4e1c24bf", "AQAAAAEAACcQAAAAEJE54AEQjVB9fGeYJo4BOEPVyma7odEyifslE90PdWvZa4l7w3F+ew2LV5V2Ub1zzw==", "af527741-9e42-441b-ac97-06a589c6ef55" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e4b2bfe-e38a-45bd-83ec-d4b027404017", "AQAAAAEAACcQAAAAEKLlrBAJb0DBsVKPzaURQSsgD7Gq4AwnKms0d8Zu6qll8OGpPgqg0hT0ud3AMf5xAQ==", "80f0f032-9114-4a71-a6c5-bf26313619e2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status_Id",
                table: "Orders",
                column: "Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_Status_Id",
                table: "Orders",
                column: "Status_Id",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
