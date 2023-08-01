using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "Employee first name"),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "Employee last name"),
                    PictureName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, comment: "Picture name of the user (nullable)"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Is active employee (soft delete property)"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "Extention of identity user");

            migrationBuilder.CreateTable(
                name: "ColorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Color model name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorModel", x => x.Id);
                },
                comment: "Printing color model");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Material type name"),
                    Width = table.Column<double>(type: "float", nullable: false, comment: "Material width"),
                    Lenght = table.Column<double>(type: "float", nullable: false, comment: "Material lenght"),
                    MeasureUnit = table.Column<int>(type: "int", nullable: false, comment: "Material measure unit (enumeration)"),
                    Price = table.Column<decimal>(type: "money", nullable: false, comment: "Material price"),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Material current quantit in stock"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                },
                comment: "Мaterial on which it is printed");

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Position name"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Soft delete property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                },
                comment: "Office position");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Color primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "Color type name"),
                    InStock = table.Column<int>(type: "int", nullable: false, comment: "Color current quantit in stock"),
                    Price = table.Column<decimal>(type: "money", nullable: false, comment: "Color price"),
                    ColorModelId = table.Column<int>(type: "int", nullable: false, comment: "Color's color model id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_ColorModel_ColorModelId",
                        column: x => x.ColorModelId,
                        principalTable: "ColorModel",
                        principalColumn: "Id");
                },
                comment: "Color");

            migrationBuilder.CreateTable(
                name: "MaterialsColorModels",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false, comment: "Material id (primary key)"),
                    ColorModelId = table.Column<int>(type: "int", nullable: false, comment: "Color model id (primary key)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsColorModels", x => new { x.MaterialId, x.ColorModelId });
                    table.ForeignKey(
                        name: "FK_MaterialsColorModels_ColorModel_ColorModelId",
                        column: x => x.ColorModelId,
                        principalTable: "ColorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialsColorModels_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Machine material and color model connecting table with article colors");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionId = table.Column<int>(type: "int", nullable: false, comment: "Employee office position id"),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Employee application user id"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete property")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Employee entity");

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Printing machine name"),
                    Model = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true, comment: "Printing machine model (optional)"),
                    PrintTime = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Machine printing time for single unit"),
                    MaterialId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to MaterialColorModel table"),
                    ColorModelId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to MaterialColorModel table"),
                    MaterialPerPrint = table.Column<double>(type: "float", nullable: false, comment: "Material required for single print"),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "Current status of the machine (has default value)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_MaterialsColorModels_MaterialId_ColorModelId",
                        columns: x => new { x.MaterialId, x.ColorModelId },
                        principalTable: "MaterialsColorModels",
                        principalColumns: new[] { "MaterialId", "ColorModelId" },
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Printing machine");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Client primary key"),
                    ClientNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Client name"),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Client e-mail"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Client phone number"),
                    MerchantId = table.Column<int>(type: "int", nullable: false, comment: "Client's merchant id"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete propery")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Employees_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Printing house client");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Article primary key."),
                    ArticleNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Article custom number"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false, comment: "Article name."),
                    ImageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Name of design image"),
                    Length = table.Column<double>(type: "float", nullable: false, comment: "Article length"),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Article owner id"),
                    MaterialId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to MaterialColorModel table"),
                    ColorModelId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to MaterialColorModel table"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete boolean property")
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
                    table.ForeignKey(
                        name: "FK_Articles_MaterialsColorModels_MaterialId_ColorModelId",
                        columns: x => new { x.MaterialId, x.ColorModelId },
                        principalTable: "MaterialsColorModels",
                        principalColumns: new[] { "MaterialId", "ColorModelId" },
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Particular client article ready for print.");

            migrationBuilder.CreateTable(
                name: "ArticleColors",
                columns: table => new
                {
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Article id"),
                    ColorId = table.Column<int>(type: "int", nullable: false, comment: "Color id"),
                    ColorQuantity = table.Column<double>(type: "float", nullable: false, comment: "Required color quantity for single print of article")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleColors", x => new { x.ArticleId, x.ColorId });
                    table.ForeignKey(
                        name: "FK_ArticleColors_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleColors_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Article color with required quantity");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Order primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Order article id"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Order article quantity"),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "DateTime of order creation"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Order deadline date if required from the client"),
                    ExpectedPrintDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Order expected print date"),
                    ExpectedPrintDuration = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Expected time needed for printing"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Order current status"),
                    Comment = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true, comment: "Additional information about the order."),
                    MachineId = table.Column<int>(type: "int", nullable: true, comment: "Expected printing machine Id for the order")
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
                    table.ForeignKey(
                        name: "FK_Orders_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id");
                },
                comment: "Order from client for print");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureName", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"), 0, "c85e382a-1570-459a-b462-561378b05f21", "admin@mail.com", false, "Admin", "Petrov", false, null, "ADMIN@MAIL.COM", "ADMIN123", "AQAAAAEAACcQAAAAEGwxjJIUYZui17WUdCkKbBIAoaFp2bIFpDjZjhbH1YaWKavhsIG3qwt7zFcGmZXWFw==", null, false, null, "d2ecdcca-b1e6-4015-aaa1-17c22a17e6b3", false, "Admin123" },
                    { new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"), 0, "ce2abfad-62e9-4bdc-9253-5c4de87e2a13", "empl1@mail.com", false, "Empl", "Nikolov", false, null, "EMPL1@MAIL.COM", "EMPLOYEE1", "AQAAAAEAACcQAAAAEA6JzgDCYAu7f5e3IKfMhF2fYBUH2Auhbdv416qVpujuv3yljYpUbWZ7LmXCaPGHBg==", null, false, null, "455036d5-b858-4330-83bb-d9bbe1e7d7a0", false, "Employee1" },
                    { new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"), 0, "240346e6-5157-4338-a8f3-37980e43c256", "printer1@mail.com", false, "Printer", "Georgiev", false, null, "PRINTER1@MAIL.COM", "PRINTER1", "AQAAAAEAACcQAAAAEM7v+SXRWnPch7ZtjBYHzlUNhZxE86kYxb4TrP3eFCxwOcGuCPHF9VOIYPJePXI3FQ==", null, false, null, "636d473a-af8e-4b21-b069-02f511f4be73", false, "Printer1" },
                    { new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"), 0, "179c04dd-0f59-4f8a-9035-e53784e20a5e", "merchant1@mail.com", false, "Merchant", "Georgiev", false, null, "MERCHANT1@MAIL.COM", "MERCHANT1", "AQAAAAEAACcQAAAAECtbHhY2qjFLgWaOWKEeTp6zXN24sKknWKV6UJzurNBUU6lgvWIPz0QDb0bAgelW4A==", null, false, null, "ff91b260-0ab1-48c3-b7dd-ecb740dfce74", false, "Merchant1" }
                });

            migrationBuilder.InsertData(
                table: "ColorModel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "RGB" },
                    { 2, "CMYK" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "InStock", "IsActive", "Lenght", "MeasureUnit", "Price", "Type", "Width" },
                values: new object[,]
                {
                    { 1, 10000, true, 0.59399999999999997, 1, 1m, "Plain paper A2", 0.41999999999999998 },
                    { 2, 100, true, 10.0, 0, 1500.50m, "Vinil 2m", 2.0 },
                    { 3, 100, true, 1000.0, 0, 850m, "Nylon 20cm", 0.20000000000000001 }
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
                    { 5, true, "Manager" },
                    { 6, true, "Printer" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorModelId", "InStock", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 1, 250, 50m, "Red" },
                    { 2, 1, 300, 48m, "Green" },
                    { 3, 1, 280, 57m, "Blue" },
                    { 4, 2, 180, 52m, "Cyan" },
                    { 5, 2, 200, 55m, "Magenta" },
                    { 6, 2, 200, 47m, "Yellow" },
                    { 7, 2, 230, 40m, "Black" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ApplicationUserId", "IsActive", "PositionId" },
                values: new object[,]
                {
                    { 1, new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"), true, 1 },
                    { 2, new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"), true, 2 },
                    { 3, new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"), true, 6 }
                });

            migrationBuilder.InsertData(
                table: "MaterialsColorModels",
                columns: new[] { "ColorModelId", "MaterialId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "IsActive", "MerchantId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("cb76cf2f-c998-459a-83aa-46035256deea"), "client@email.com", true, 2, "Client 2", "+056568645" },
                    { new Guid("ffbddf06-701d-49f2-8e4b-df760d13b2a6"), "TestClient@email.com", true, 2, "Test Client", "1234567890" }
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "ColorModelId", "MaterialId", "MaterialPerPrint", "Model", "Name", "PrintTime" },
                values: new object[,]
                {
                    { 1, 2, 2, 5.0, null, "Machine 1", new TimeSpan(0, 0, 3, 0, 0) },
                    { 2, 2, 2, 5.0, null, "Machine 2", new TimeSpan(0, 0, 2, 30, 0) },
                    { 3, 1, 1, 1.0, null, "Machine 3", new TimeSpan(0, 0, 0, 3, 0) },
                    { 4, 2, 3, 500.0, null, "Machine 4", new TimeSpan(0, 0, 40, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), "101.2", new Guid("ffbddf06-701d-49f2-8e4b-df760d13b2a6"), 2, "101.2_1.jpg", true, 0.29999999999999999, 3, "Salami Teleshki 0.3" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), "101.1", new Guid("ffbddf06-701d-49f2-8e4b-df760d13b2a6"), 2, "101.1_1.jpg", true, 4.5, 2, "Vinil Article" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleNumber", "ClientId", "ColorModelId", "ImageName", "IsActive", "Length", "MaterialId", "Name" },
                values: new object[] { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), "102.1", new Guid("cb76cf2f-c998-459a-83aa-46035256deea"), 1, "102.1_1.webp", true, 1.0, 1, "Movie poster A2" });

            migrationBuilder.InsertData(
                table: "ArticleColors",
                columns: new[] { "ArticleId", "ColorId", "ColorQuantity" },
                values: new object[,]
                {
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 4, 0.20000000000000001 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 5, 0.19 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 6, 0.089999999999999997 },
                    { new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), 7, 0.10000000000000001 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 4, 0.080000000000000002 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 5, 0.17000000000000001 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 6, 0.089999999999999997 },
                    { new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), 7, 0.10000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 1, 0.0030000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 2, 0.0070000000000000001 },
                    { new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), 3, 0.0089999999999999993 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleColors_ColorId",
                table: "ArticleColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ClientId",
                table: "Articles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_MaterialId_ColorModelId",
                table: "Articles",
                columns: new[] { "MaterialId", "ColorModelId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MerchantId",
                table: "Clients",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Name",
                table: "Clients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Color_ColorModelId",
                table: "Color",
                column: "ColorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MaterialId_ColorModelId",
                table: "Machines",
                columns: new[] { "MaterialId", "ColorModelId" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsColorModels_ColorModelId",
                table: "MaterialsColorModels",
                column: "ColorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ArticleId",
                table: "Orders",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MachineId",
                table: "Orders",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleColors");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "MaterialsColorModels");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ColorModel");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
