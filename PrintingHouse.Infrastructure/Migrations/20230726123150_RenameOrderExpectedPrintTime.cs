using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class RenameOrderExpectedPrintTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedPrintTime",
                table: "Orders",
                newName: "ExpectedPrintDuration");

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
                table: "Machines",
                keyColumn: "Id",
                keyValue: 4,
                column: "MaterialPerPrint",
                value: 500.0);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 0.59399999999999997, 0.41999999999999998 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 10.0, 2.0 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 1000.0, 0.20000000000000001 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedPrintDuration",
                table: "Orders",
                newName: "ExpectedPrintTime");

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

            migrationBuilder.UpdateData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 4,
                column: "MaterialPerPrint",
                value: 1.0);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 594.0, 420.0 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 0.01, 0.002 });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Lenght", "Width" },
                values: new object[] { 1.0, 0.00020000000000000001 });
        }
    }
}
