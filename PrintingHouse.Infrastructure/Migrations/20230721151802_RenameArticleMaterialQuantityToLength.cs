using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class RenameArticleMaterialQuantityToLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialQuantity",
                table: "Articles");

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "Articles",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Article length");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e218c85-146f-4017-ace0-783bedc3ae73", "AQAAAAEAACcQAAAAEMbxAGdBRWJVFup4VP26MtaLtEhr+EljBzXUIwVqcRNF9BbuweFg2EZgRaJu1ATnUw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35742f91-ea11-42f6-806d-02f8dc4e6299", "AQAAAAEAACcQAAAAEBzMRQzcHdUhqelZ9EvhEZ/aCNvKmGFsXmG2sw1NhY9tx+thIz6hYP854XfvTnrcTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "60de12ab-5982-4b4a-be1d-255de3b6bd7c", "AQAAAAEAACcQAAAAEEDGLrZpVVorxZvefFm5jLkC+9ILprg76492bCWGKiiJbsqimPjd7qNGEU4dhdgzwg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Articles");

            migrationBuilder.AddColumn<double>(
                name: "MaterialQuantity",
                table: "Articles",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Required material lenght");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ebaf95a4-441e-481e-b849-133654ad2a3a", "AQAAAAEAACcQAAAAEN9n3k0JlADVFwdnQ4vFM5WZJKNCR+yyvwn9bXujZhGYWxF6s4gqk79UCha+m7YR6A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3b5b867b-1aea-494b-8063-ed79b7035a1d", "AQAAAAEAACcQAAAAEIHGFmttovUq5JGXxeoWjHXUS9fZ9EUOh+pN9NfQjVdjBu0xBf1AUbrZUgIbo/0kXg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6204023f-9076-4731-a8e1-fd3600535fdd", "AQAAAAEAACcQAAAAEL7zYt8T/GP+zXvP5uFgECYs9WISsxY8F/fS2Or5j7v+guH9yrOCLFlUpqoa4wE95g==" });
        }
    }
}
