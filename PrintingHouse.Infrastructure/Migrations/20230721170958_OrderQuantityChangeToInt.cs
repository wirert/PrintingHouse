using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class OrderQuantityChangeToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Order article quantity",
                oldClrType: typeof(double),
                oldType: "float",
                oldComment: "Order article quantity");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
