using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddNavPropertyBetweenColorModelAndColorRenamedFromConsumable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Articles_ArticleId",
                table: "ArticlesConsumables");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Consumables_ConsumableId",
                table: "ArticlesConsumables");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesConsumables_ConsumableId",
                table: "ArticlesConsumables");

            migrationBuilder.DropColumn(
                name: "ConsumableId",
                table: "ArticlesConsumables");

            migrationBuilder.DropColumn(
                name: "ConsumableQuantity",
                table: "ArticlesConsumables");

            migrationBuilder.AlterTable(
                name: "ArticlesConsumables",
                comment: "Article color with quantity (connecting table)",
                oldComment: "Article consumable with quantity (connecting table");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "ArticlesConsumables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Color id");

            migrationBuilder.AddColumn<double>(
                name: "ColorQuantity",
                table: "ArticlesConsumables",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Required color quantity for single print of article");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables",
                columns: new[] { "ArticleId", "ColorId" });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Consumable primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Consumable type name"),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Consumable current quantit in stock"),
                    Price = table.Column<decimal>(type: "money", nullable: false, comment: "Consumable price"),
                    ColorModelId = table.Column<int>(type: "int", nullable: false, comment: "Color model id from which that color is")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_ColorModels_ColorModelId",
                        column: x => x.ColorModelId,
                        principalTable: "ColorModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Machine consumable");

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "ColorModelId", "InStock", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 1, 104, 50m, "Red" },
                    { 2, 1, 92, 48m, "Green" },
                    { 3, 1, 67, 57m, "Blue" },
                    { 4, 2, 47, 52m, "Cyan" },
                    { 5, 2, 38, 55m, "Magenta" },
                    { 6, 2, 50, 47m, "Yellow" },
                    { 7, 2, 60, 40m, "Black" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesConsumables_ColorId",
                table: "ArticlesConsumables",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ColorModelId",
                table: "Colors",
                column: "ColorModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesConsumables_Articles_ArticleId",
                table: "ArticlesConsumables",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesConsumables_Colors_ColorId",
                table: "ArticlesConsumables",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Articles_ArticleId",
                table: "ArticlesConsumables");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Colors_ColorId",
                table: "ArticlesConsumables");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables");

            migrationBuilder.DropIndex(
                name: "IX_ArticlesConsumables_ColorId",
                table: "ArticlesConsumables");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ArticlesConsumables");

            migrationBuilder.DropColumn(
                name: "ColorQuantity",
                table: "ArticlesConsumables");

            migrationBuilder.AlterTable(
                name: "ArticlesConsumables",
                comment: "Article consumable with quantity (connecting table",
                oldComment: "Article color with quantity (connecting table)");

            migrationBuilder.AddColumn<int>(
                name: "ConsumableId",
                table: "ArticlesConsumables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Consumable id");

            migrationBuilder.AddColumn<double>(
                name: "ConsumableQuantity",
                table: "ArticlesConsumables",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                comment: "Required consumable quantity for single print of article");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables",
                columns: new[] { "ArticleId", "ConsumableId" });

            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Consumable primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Consumable current quantit in stock"),
                    Price = table.Column<decimal>(type: "money", nullable: false, comment: "Consumable price"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Consumable type name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
                },
                comment: "Machine consumable");

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

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesConsumables_ConsumableId",
                table: "ArticlesConsumables",
                column: "ConsumableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesConsumables_Articles_ArticleId",
                table: "ArticlesConsumables",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesConsumables_Consumables_ConsumableId",
                table: "ArticlesConsumables",
                column: "ConsumableId",
                principalTable: "Consumables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
