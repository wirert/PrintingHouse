using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class seedArticlesAndArticleColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "25805e82-310e-45e9-b8b1-c049f58fd8b7", "AQAAAAEAACcQAAAAEPS90qZyXxM4OIWMZXHNjNWhk/qU/Pemc8ePM3u6fAd9dPOQZClpQ2ZsLzoOGggo9Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e329a91f-d860-4f25-aee8-de4cb366488a", "AQAAAAEAACcQAAAAEAxtVlEhJ28YlnxBDNKqIy16I2EYVNURfYATku3kqoFM2DGcBsaV14v485ihFLGoDg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f93fdc59-b908-4418-9d9c-9070c25f4fd1", "AQAAAAEAACcQAAAAEG5VblBo4vkLZzHpPhbCvoZIA+2NJvoAAcyXnKzoUpBnaPszrgQDhOyYgPnZy4VebQ==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"), 0, "36d8cc97-dc9d-4cc6-87b5-a200584a14e3", "printer1@mail.com", false, "Printer", "Georgiev", false, null, "PRINTER1@MAIL.COM", "PRINTER1", "AQAAAAEAACcQAAAAEHssjMeIBfcX9objBUESYAquXL1G78QdJz4sh8h70zALruwBlT7wPkDRAuMjANZ2qA==", null, false, null, "636d473a-af8e-4b21-b069-02f511f4be73", false, "Printer1" });

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "InStock",
                value: 250);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "InStock",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 3,
                column: "InStock",
                value: 280);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 4,
                column: "InStock",
                value: 180);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 5,
                column: "InStock",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 6,
                column: "InStock",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 7,
                column: "InStock",
                value: 230);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ApplicationUserId", "IsActive", "PositionId" },
                values: new object[] { 2, new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"), true, 2 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "InStock",
                value: 100);

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 6, true, "Printer" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "IsActive", "MerchantId", "Name", "PhoneNumber" },
                values: new object[] { 101, "TestClient@email.com", true, 2, "Test Client", "1234567890" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "IsActive", "MerchantId", "Name", "PhoneNumber" },
                values: new object[] { 102, "client@email.com", true, 2, "Client 2", "+056568645" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ApplicationUserId", "IsActive", "PositionId" },
                values: new object[] { 3, new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"), true, 6 });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), "101.2", 101, 2, "101.2_1.jpg", true, 0.29999999999999999, 3, "Salami Teleshki 0.3" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), "101.1", 101, 2, "101.1_1.jpg", true, 4.5, 2, "Vinil Article" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), "102.1", 102, 1, "102.1_1.webp", true, 1.0, 1, "Movie poster A2" });

            migrationBuilder.InsertData(
                table: "ArticleColors",
                columns: new[] { "ArticleId", "ColorId", "ColorQuantity" },
                values: new object[,]
                {
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 4, 0.20000000000000001 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 5, 0.19 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 6, 0.089999999999999997 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 7, 0.10000000000000001 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 4, 0.080000000000000002 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 5, 0.17000000000000001 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 6, 0.089999999999999997 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 7, 0.10000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 1, 0.0030000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 2, 0.0070000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 3, 0.0089999999999999993 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 4 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 5 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 6 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 7 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 4 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 5 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 6 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 7 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 1 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 2 });

            migrationBuilder.DeleteData(
                table: "ArticleColors",
                keyColumns: new[] { "ArticleId", "ColorId" },
                keyValues: new object[] { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 3 });

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b8d5417-8336-4930-849d-e3ed66f0e455", "AQAAAAEAACcQAAAAEDlRSDIyRV5Ven1ULLZ9NjCAmxcTkqClsK3TRTQcOeMwcfGyhGC3MU7dJw4t/Ea3ow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8bf933cd-1eab-43a4-9fa6-db19f8ddfa25", "AQAAAAEAACcQAAAAEPEZnZBtfW9z4rLS4uPVnF0ACA1dReBKrq8udhVIIw/U5toH2TTddYIa+q4jxvzgAw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5886c9d-f7cc-4f5e-85e2-1a156a9ee98e", "AQAAAAEAACcQAAAAEKGj8acaVYXjj3naEg8OC8s/lEaJ4QhqdpgAN0tw83bR4giCl+UCel8EMPNpRuYvbw==" });

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "InStock",
                value: 104);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "InStock",
                value: 92);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 3,
                column: "InStock",
                value: 67);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 4,
                column: "InStock",
                value: 47);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 5,
                column: "InStock",
                value: 38);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 6,
                column: "InStock",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 7,
                column: "InStock",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3,
                column: "InStock",
                value: 20);
        }
    }
}
