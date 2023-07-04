using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class SeedMachinesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "ColorModelId", "MaterialId", "MaterialPerPrint", "Model", "Name", "PrintTime", "Status" },
                values: new object[,]
                {
                    { 1, 2, 2, 5.0, null, "Machine 1", new TimeSpan(0, 0, 3, 0, 0), 0 },
                    { 2, 2, 2, 5.0, null, "Machine 2", new TimeSpan(0, 0, 2, 30, 0), 0 },
                    { 3, 1, 1, 1.0, null, "Machine 3", new TimeSpan(0, 0, 0, 3, 0), 0 },
                    { 4, 2, 3, 1.0, null, "Machine 4", new TimeSpan(0, 0, 40, 0, 0), 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Machines",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
