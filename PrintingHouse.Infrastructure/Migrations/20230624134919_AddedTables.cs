using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Client primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Client name"),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Client e-mail"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Client phone number"),
                    MerchantId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Client's merchant id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Printing house client");

            migrationBuilder.CreateTable(
                name: "Consumables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Consumable primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "Consumable type (enumeration)"),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Consumable current quantit in stock"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Consumable price")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
                },
                comment: "Machine consumable");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "Material type (enumeration)"),
                    Width = table.Column<double>(type: "float", nullable: false, comment: "Material width"),
                    Lenght = table.Column<double>(type: "float", nullable: false, comment: "Material lenght"),
                    MeasureUnit = table.Column<int>(type: "int", nullable: false, comment: "Material measure unit (enumeration)"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Material price"),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Material current quantit in stock")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                },
                comment: "Мaterial on which it is printed");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Article primary key.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Article name."),
                    Material = table.Column<int>(type: "int", nullable: false, comment: "Article material"),
                    ColorModel = table.Column<int>(type: "int", nullable: false, comment: "Article color model"),
                    ClientId = table.Column<int>(type: "int", nullable: false, comment: "Article owner id"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Soft delete boolean property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Particular client article ready for print.");

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Printing machine name"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Printing machine model (optional"),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Machine printing time for single unit"),
                    ColorModel = table.Column<int>(type: "int", nullable: false, comment: "Machine working color model"),
                    MaterialId = table.Column<int>(type: "int", nullable: false, comment: "Machine printing material id."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Current status of the machine")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Printing machine");

            migrationBuilder.CreateTable(
                name: "ArticlesConsumables",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false, comment: "Article id"),
                    ConsumableId = table.Column<int>(type: "int", nullable: false, comment: "Consumable id"),
                    ConsumableQuantity = table.Column<double>(type: "float", nullable: false, comment: "Required consumable quantity for single print of article")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesConsumables", x => new { x.ArticleId, x.ConsumableId });
                    table.ForeignKey(
                        name: "FK_ArticlesConsumables_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlesConsumables_Consumables_ConsumableId",
                        column: x => x.ConsumableId,
                        principalTable: "Consumables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Article consumable with quantity (connecting table");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Order primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: false, comment: "Order article id"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Order article quantity"),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime of order creation"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Order expected end date if required from the client"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Order current status"),
                    Comment = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true, comment: "Additional information about the order.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Order from client for print");

            migrationBuilder.CreateTable(
                name: "MachinesArticles",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false, comment: "Machine primary key"),
                    ArticleId = table.Column<int>(type: "int", nullable: false, comment: "Article primary key")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinesArticles", x => new { x.MachineId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_MachinesArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachinesArticles_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Connecting table between machines and articles (many to many)");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ClientId",
                table: "Articles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesConsumables_ConsumableId",
                table: "ArticlesConsumables",
                column: "ConsumableId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MerchantId",
                table: "Clients",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MaterialId",
                table: "Machines",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MachinesArticles_ArticleId",
                table: "MachinesArticles",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ArticleId",
                table: "Orders",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlesConsumables");

            migrationBuilder.DropTable(
                name: "MachinesArticles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Consumables");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
