using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddArticleNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleNumber",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4af0a813-5846-45a6-a45e-0fb4e3c0066e", "AQAAAAEAACcQAAAAELOsi9oaDLu/N+oZoGIajBIgFnHXddWhO3UbF10HNM+IhPZ/mImWZPtgL9llPjG9gg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2060674b-8dcc-4338-b769-7884043633c7", "AQAAAAEAACcQAAAAECCYmxqeeTBbOIb3jq2JLCVSc2JTX1mMqWT1Htk52A8VWtSQke6ER2AmlTo5oQW06Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "281420bb-c528-4d6c-971d-eb6503d56d07", "AQAAAAEAACcQAAAAELTkdfB3LHGZhmmXmknyTmj7WUAbBFsx5vnGEkpgL2fmRG80kj68ZzU48LiDO6sWjw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleNumber",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f391a5b2-078f-4bac-aecf-bf071b646306", "AQAAAAEAACcQAAAAEHJhxuLXWf5igzFtlPGT/vOgHD31VzAvG3vuAFMYvUQy2DFe+KWSHWLT9R5kHKE/vA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d802d894-dc3d-4b4b-8735-6e97e4496c4b", "AQAAAAEAACcQAAAAEH2jMWzk77ERO8cewJZA4gtIukkIAoRcyWhiyq7actAPY5H8yfRk7HZRUj8/rjMivQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1436b11c-c659-443b-a780-58d9e2adf477", "AQAAAAEAACcQAAAAEGO/XRqDFZPqxHxaLGqHnMSztXttaUzdKUmQghMQmlOe/mOkcmYG1XkvR/UnG2TO+g==" });
        }
    }
}
