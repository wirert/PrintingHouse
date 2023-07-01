using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class FirstAndLastNameNullableAndRemoveEnumerationFromConsumablesAndMaterialsTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Materials",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "Material type name",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Material type (enumeration)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Consumables",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Consumable type name",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Consumable type (enumeration)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ColorModels",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                comment: "Color model name",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Color model name (enumeration)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "Employee last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Employee last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "Employee first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "Employee first name");

            migrationBuilder.AddColumn<string>(
                name: "DesignUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Url to design image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignUrl",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Materials",
                type: "int",
                nullable: false,
                comment: "Material type (enumeration)",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldComment: "Material type name");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Consumables",
                type: "int",
                nullable: false,
                comment: "Consumable type (enumeration)",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Consumable type name");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "ColorModels",
                type: "int",
                nullable: false,
                comment: "Color model name (enumeration)",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldComment: "Color model name");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "Employee last name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Employee last name");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "Employee first name",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "Employee first name");
        }
    }
}
