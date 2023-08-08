using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddMachinePrintOrderNumberToOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachinePrintOrderNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The order number in machine queue");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "611b1ed0-b007-4399-ac08-1fff08838c4d", "AQAAAAEAACcQAAAAEJjLhqQfX3fpOdEBdLKZdcPB9EticMiJds+iOvPsF3valo3x/GmNWzInKjaswsZIpQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1a79d8ed-585e-46eb-9500-b88d9bd03fb2", "AQAAAAEAACcQAAAAEAFzR2tzQbftich6IBWHs8+9+Sfe7LwgEy5WiBD9QIFAAGs+PuZjQ2x4Es8TFdpe8w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a7d12e1f-6f90-4cee-ab9e-debc4e6a60d6", "AQAAAAEAACcQAAAAEGlWtefk6Y8qr2vJ0qSZy/kbqUEaYA4m2zcvafbaRydPmqhTSUp7cFW0DqC3uAQ+FA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3e2fa525-47d7-4266-98e2-df12accc781d", "AQAAAAEAACcQAAAAEEPFt+LSin1zZmrEE2kW6r9nymI7P5dFfrE5BqbBt8tOZXCmHQeLzhUfFCB2MUO0Vg==" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4567) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                columns: new[] { "EndDate", "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 18, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 12, 24, 55, 541, DateTimeKind.Utc).AddTicks(4557) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 11, 59, 55, 541, DateTimeKind.Utc).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 9, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 8, 11, 54, 55, 541, DateTimeKind.Utc).AddTicks(4609) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachinePrintOrderNumber",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1dc6c20d-b019-4d0f-a0ca-c4173bf7cbfd", "AQAAAAEAACcQAAAAENP8hm2e4i96mTen95P6TFLL9r7TfeCcxkCy1rN7ItMk4lKN7KdVDABK89XmFTAbIQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88e8dff2-d884-4fdd-bf81-3847e457a256", "AQAAAAEAACcQAAAAEOtWCKnidL9oesO+Xn8qwyaq/KkKBLHhQdhcZzQ1cVBeiN6q9AXho5bKlj+GhQ77IQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "88807086-c6c2-4d62-b476-9ed313c4c47d", "AQAAAAEAACcQAAAAEP+p3L2yGHcczPnWYxKWa09EkLRSbZf3cAIvWKH67sc21CSbOTM7PVGJPXxrYjJ0bA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c110e2bf-c825-438f-b8fb-9d1467693295", "AQAAAAEAACcQAAAAECEAN+fG42UIiviqCWd+d1bmJabuNmiWbLD496LOipHEAdDtCCVTugSEQDHyKQkcqA==" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7735) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7708) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                columns: new[] { "EndDate", "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 20, 17, 48, 939, DateTimeKind.Utc).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 19, 52, 48, 939, DateTimeKind.Utc).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7749) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                columns: new[] { "ExpectedPrintDate", "OrderTime" },
                values: new object[] { new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2023, 8, 7, 19, 47, 48, 939, DateTimeKind.Utc).AddTicks(7739) });
        }
    }
}
