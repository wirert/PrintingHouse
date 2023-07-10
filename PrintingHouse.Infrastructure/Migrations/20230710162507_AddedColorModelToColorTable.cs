using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddedColorModelToColorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorModelId",
                table: "Color",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Color's color model id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3d28714a-4f0d-4eab-ab33-e618bf666cf4", "AQAAAAEAACcQAAAAEAicMPwBWTGoaA1onBToen1Ff70sGwEjp11elaCAfpThXeMdvP0IOJr/bjts1oxTxg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b2a29da3-f85d-4f1a-8fd3-9099944c520c", "AQAAAAEAACcQAAAAEMkeDSbg9yPRLpQJr/Wt5lLZdC8ctgcJ17tF8kcF+wmGNYADb9ZUueyKraAQH3dOOw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f10c9eb-4939-4838-9762-ca8abc6b7af2", "AQAAAAEAACcQAAAAEKuXrU2ZCrSdG4o9mMzKLeXppfPAlHIsgHZYT7Yt39E6xjEmSDICoxuQsMZS/DdECw==" });

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColorModelId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "ColorModelId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 3,
                column: "ColorModelId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 4,
                column: "ColorModelId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 5,
                column: "ColorModelId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 6,
                column: "ColorModelId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 7,
                column: "ColorModelId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Color_ColorModelId",
                table: "Color",
                column: "ColorModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Color_ColorModel_ColorModelId",
                table: "Color",
                column: "ColorModelId",
                principalTable: "ColorModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Color_ColorModel_ColorModelId",
                table: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Color_ColorModelId",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "ColorModelId",
                table: "Color");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5f89b31-e136-4fa5-af85-6d894587d1df", "AQAAAAEAACcQAAAAEInWF6oTX2iEiojPVpBv3VMgZypmuYVTue7NClyQjvjIuHRzW/uEzCFqiMOci6GXEw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5eee9b54-3fe0-49ae-9e73-06ccb35c2aef", "AQAAAAEAACcQAAAAEEx5yhBKtCJfxQF/zUe9cV2xf+kBb2r2QkRC9P32A1vHS1prp+aTJfKGdOG4CnzBfw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1996cdb1-a667-423e-abc4-966b97ca4175", "AQAAAAEAACcQAAAAEHIr25deIQOlw8IJUoHP50xNyegO9SEd4TJLKIOc0IMS+87mh+5vMgYuCDZvqsfwZQ==" });
        }
    }
}
