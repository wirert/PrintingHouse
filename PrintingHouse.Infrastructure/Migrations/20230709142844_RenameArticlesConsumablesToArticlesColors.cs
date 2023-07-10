using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class RenameArticlesConsumablesToArticlesColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Articles_ArticleId",
                table: "ArticlesConsumables");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesConsumables_Colors_ColorId",
                table: "ArticlesConsumables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables");

            migrationBuilder.RenameTable(
                name: "ArticlesConsumables",
                newName: "ArticlesColors");

            migrationBuilder.RenameIndex(
                name: "IX_ArticlesConsumables_ColorId",
                table: "ArticlesColors",
                newName: "IX_ArticlesColors_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesColors",
                table: "ArticlesColors",
                columns: new[] { "ArticleId", "ColorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesColors_Articles_ArticleId",
                table: "ArticlesColors",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesColors_Colors_ColorId",
                table: "ArticlesColors",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesColors_Articles_ArticleId",
                table: "ArticlesColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesColors_Colors_ColorId",
                table: "ArticlesColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesColors",
                table: "ArticlesColors");

            migrationBuilder.RenameTable(
                name: "ArticlesColors",
                newName: "ArticlesConsumables");

            migrationBuilder.RenameIndex(
                name: "IX_ArticlesColors_ColorId",
                table: "ArticlesConsumables",
                newName: "IX_ArticlesConsumables_ColorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesConsumables",
                table: "ArticlesConsumables",
                columns: new[] { "ArticleId", "ColorId" });

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
    }
}
