using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddMachineAndExpectedDateToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Orders",
                type: "float",
                nullable: false,
                comment: "Order article quantity",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Order article quantity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                comment: "Order deadline date if required from the client",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Order expected end date if required from the client");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedPrintDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                comment: "Order expected print date");

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Expected printing machine Id for the order");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Articles",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                comment: "Article custom number",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1dee99aa-c368-4969-b42b-3c4371c62f85", "AQAAAAEAACcQAAAAEIOOfyd7iGonEtxPHTDHs+F9/x+k7MWzpr9Zp1P3znQm0gpTxg+3QsB5Oa/frmR20g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9704c135-1151-4bab-bcb4-3e34fe75e7ef", "AQAAAAEAACcQAAAAEMSYDz1hKM/0CZ0UzUgnPfRkzufphqPTO5WNSM+brADbQKhSAD5RxCvurPIHkYD1nA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1cba2b98-67ab-4406-a003-70e1a00872b3", "AQAAAAEAACcQAAAAEDXVwJ30ZPJmmNsisSTo7snBVe/wPkPaRLqRM/reMEuhv5wCyVSJVVQWEW2y+8DgcQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MachineId",
                table: "Orders",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MachineId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ExpectedPrintDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Order article quantity",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Order article quantity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                comment: "Order expected end date if required from the client",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "Order deadline date if required from the client");

            migrationBuilder.AlterColumn<string>(
                name: "ArticleNumber",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Article custom number");

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
    }
}
