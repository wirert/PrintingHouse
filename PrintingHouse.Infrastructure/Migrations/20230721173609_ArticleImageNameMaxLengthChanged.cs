using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class ArticleImageNameMaxLengthChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Articles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Name of design image",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldComment: "Name of design image");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b82a539-a96b-4030-b42b-b0264247c920", "AQAAAAEAACcQAAAAEN7fveRKYeU5Bj060qr37gkHyW0ZWOuo2PtVHyO0RuplDGJGY1md++T6utIAkR2tFg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "740a311d-1b92-4026-9552-b00755c4e2ce", "AQAAAAEAACcQAAAAEAqU3O5VwOvNQOOzZrbJlx1tUbx9bJRJ31V3Kqw7iRHE/bzDbkShv7pm82y8CXcNow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ffeffa2a-e816-452f-9e6b-bc66c870db65", "AQAAAAEAACcQAAAAEPSdy6t04zKeBoXdrIp0Ncuvc2vQEDlBIDwuQjzoKx7ff6cf005fThYf0y3G3NJ1Jg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageName",
                table: "Articles",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                comment: "Name of design image",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Name of design image");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a125bef2-c25a-4f38-98a7-036489fcbeb7", "AQAAAAEAACcQAAAAEOD2IOkRjwfk7wHLAXDNPXfu52wlKcakc4KuDXK57JmBtWbzqa5/Gsqch0HQ5ROBQw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2f5d45a-055b-49f4-9269-5eadf1b6ae5a", "AQAAAAEAACcQAAAAEASd82nRQrzX1LhnnUxUO1rmVrNBJQ2dEts46QlVcQck0rNejJwyB0R/9m1imBHHdg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "09ecb0b4-d8c4-4238-89db-3e756296043c", "AQAAAAEAACcQAAAAEAaoMTpqvZn2M0vFc/7a9HW8VeGpn+PcPXcH0zOwOq92QjMkszdZ5SdzmfpRaSDLmw==" });
        }
    }
}
