using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class OrderMachineIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "Orders",
                type: "int",
                nullable: true,
                comment: "Expected printing machine Id for the order",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Expected printing machine Id for the order");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Expected printing machine Id for the order",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Expected printing machine Id for the order");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Machines_MachineId",
                table: "Orders",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
