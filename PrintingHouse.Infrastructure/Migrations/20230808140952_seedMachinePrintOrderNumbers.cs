using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class seedMachinePrintOrderNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1896b6c9-668c-46cf-96d8-81a234d7d5a9", "AQAAAAEAACcQAAAAEE3tL9kmPS9X+EUYTlpoaI2oxKL3CIhw2UU76rpel87H8vFhLxatGGvrLGxW3gBTow==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dab936a8-d868-4901-a597-561f1aeb0c16", "AQAAAAEAACcQAAAAEAb6gFogaMr38zUkwCjQGdTKI9FsQQLxhQ9iuM89/t+ElU9/+psZe/ALw+myMZF8Fw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4343d59f-6607-4227-83d8-2b596f9439f3", "AQAAAAEAACcQAAAAECYQmrEqixv+cyjtKhre/KcUPYm4lYfAAbMpGVQEBQ1G5OwnTW88t35xjixBPDegXQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2db36d4-3e09-4b89-9abe-fe40dd7d3de1", "AQAAAAEAACcQAAAAEJfTs1SPs0yHyuVFd/hAlTpKesGlo4oRsH/cf31Ay7UYlSjh6PNa42zjT3umbMNDdw==" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 1, new DateTime(2023, 8, 8, 14, 9, 51, 300, DateTimeKind.Utc).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 1, new DateTime(2023, 8, 8, 14, 9, 51, 300, DateTimeKind.Utc).AddTicks(7184) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 2, new DateTime(2023, 8, 8, 14, 44, 51, 300, DateTimeKind.Utc).AddTicks(7191) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 2, new DateTime(2023, 8, 8, 14, 19, 51, 300, DateTimeKind.Utc).AddTicks(7211) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 1, new DateTime(2023, 8, 8, 14, 9, 51, 300, DateTimeKind.Utc).AddTicks(7217) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 1, new DateTime(2023, 8, 8, 14, 14, 51, 300, DateTimeKind.Utc).AddTicks(7207) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4567) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 12, 24, 55, 541, DateTimeKind.Utc).AddTicks(4557) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 11, 59, 55, 541, DateTimeKind.Utc).AddTicks(4615) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 11, 49, 55, 541, DateTimeKind.Utc).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                columns: new[] { "MachinePrintOrderNumber", "OrderTime" },
                values: new object[] { 0, new DateTime(2023, 8, 8, 11, 54, 55, 541, DateTimeKind.Utc).AddTicks(4609) });
        }
    }
}
