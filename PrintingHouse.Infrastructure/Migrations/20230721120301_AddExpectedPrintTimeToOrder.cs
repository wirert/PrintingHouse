using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddExpectedPrintTimeToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedPrintDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Order expected print date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Order expected print date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ExpectedPrintTime",
                table: "Orders",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                comment: "Expected time needed for printing");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedPrintTime",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedPrintDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                comment: "Order expected print date",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Order expected print date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bd9070f0-4939-4c02-ab40-d65c34925ab9", "AQAAAAEAACcQAAAAEJvifyaHyk6t+9arDJ6jWhBo4Lz5fbJ/XdutxStIxtPlso84u6qnmh+PATv9F7Ck9w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53c7e294-c870-49a2-8a33-a913c99a2a01", "AQAAAAEAACcQAAAAEB7cdk9uOhR4HB/+9gVM/+61dnc/AFkuLOfKpw2fdrkN2VUi7QMW+ou3DHmgGKqS/w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e358c648-fc8c-4c0a-9f10-a7aa4ad55692", "AQAAAAEAACcQAAAAELXWjPT4HZPCiVC6smhnm31qZ9pLQEYjbl/X/Hp2TSO1yvAtA6wocIoQWQ6Ert1ecQ==" });
        }
    }
}
