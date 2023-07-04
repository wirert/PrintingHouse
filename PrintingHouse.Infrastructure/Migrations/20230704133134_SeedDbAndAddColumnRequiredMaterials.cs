using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class SeedDbAndAddColumnRequiredMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PrintTime",
                table: "Machines",
                type: "time",
                nullable: false,
                comment: "Machine printing time for single unit",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Machine printing time for single unit");

            migrationBuilder.AddColumn<double>(
                name: "MaterialPerPrint",
                table: "Machines",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Material required for single print");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                comment: "Soft delete property",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "Soft delete property");

            migrationBuilder.AddColumn<double>(
                name: "RequiredMaterial",
                table: "Articles",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Required material lenght");

            migrationBuilder.InsertData(
                table: "ColorModels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "RGB" },
                    { 2, "CMYK" }
                });

            migrationBuilder.InsertData(
                table: "Consumables",
                columns: new[] { "Id", "InStock", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 104, 50m, "Red" },
                    { 2, 92, 48m, "Green" },
                    { 3, 67, 57m, "Blue" },
                    { 4, 47, 52m, "Cyan" },
                    { 5, 38, 55m, "Magenta" },
                    { 6, 50, 47m, "Yellow" },
                    { 7, 60, 40m, "Black" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "InStock", "IsActive", "Lenght", "MeasureUnit", "Price", "Type", "Width" },
                values: new object[,]
                {
                    { 1, 10000, true, 594.0, 1, 1m, "Plain paper A2", 420.0 },
                    { 2, 100, true, 0.01, 0, 1500.50m, "Vinil 2m", 0.002 },
                    { 3, 20, true, 1.0, 0, 850m, "Nylon 20cm", 0.00020000000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Administrator" },
                    { 2, true, "Merchant" },
                    { 3, true, "Employee" },
                    { 4, true, "Designer" },
                    { 5, true, "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ColorModels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ColorModels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Consumables",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "MaterialPerPrint",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "RequiredMaterial",
                table: "Articles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrintTime",
                table: "Machines",
                type: "datetime2",
                nullable: false,
                comment: "Machine printing time for single unit",
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldComment: "Machine printing time for single unit");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "Soft delete property",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Soft delete property");
        }
    }
}
