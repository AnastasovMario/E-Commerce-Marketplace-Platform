using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceMarketplace.Infrastructure.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Paid" },
                    { 2, "Unpaid" },
                    { 3, "Unavailable" },
                    { 4, "Stocked" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 1, "+359888888888", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "ImageUrl", "Name", "OrderId", "Price", "VendorId" },
                values: new object[,]
                {
                    { 1, 1, "https://www.pdevice.com/wp-content/uploads/2015/09/Gigaset-ME-pure-600x600.jpeg", "Smartphone XZ200", null, 799.99m, 1 },
                    { 2, 2, "https://www.jottnar.com/cdn/shop/products/Productimage-Lodur-Turbulence-min_c13cd744-2711-4fa0-81d0-f312ed4a6a1a_3200x1800_crop_center.jpg?v=1681214994", "Men's Classic T-Shirt", null, 19.99m, 1 },
                    { 3, 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR3B-zF-TdhbybX7l-51SJSrfSoDZLKEgxPuN-Och_y&s", "Garden Tool Set", null, 39.95m, 1 },
                    { 4, 1, "https://cdn.anscommerce.com/catalog/brandstore/johnson/17_7_20/Sale.jpg", "Luxury Watch", null, 899.50m, 1 },
                    { 5, 4, "https://m.media-amazon.com/images/I/61x-NhdKBmL.jpg", "Cookware Set", null, 149.99m, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vendors",
                keyColumn: "Id",
                keyValue: 1);

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
        }
    }
}
