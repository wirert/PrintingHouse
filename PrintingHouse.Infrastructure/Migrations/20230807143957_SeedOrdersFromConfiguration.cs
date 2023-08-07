using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class SeedOrdersFromConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "43fdffad-e96c-4054-b497-416317572f63", "AQAAAAEAACcQAAAAEKlDhlsvonBDpv9UcuyhnKtCmhrXN+oCqIpmdDl49Ef9C4JU1/wUQKQdLKg24YDOLA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6690f626-b22a-4489-9037-c135061b406e", "AQAAAAEAACcQAAAAEBgtCKoGxqpfa5WKuir8sNJ5IUTmasMIjxsdu+28UousX9QprMEQHusqtKsC+8vV+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "75f947e8-4d09-4466-a853-1d83cc9b2714", "AQAAAAEAACcQAAAAEMEMW+FdwjXsjfan/Vj2xJthDSmZpAhRW+eBAfbhE0ZseUk9P7eN63UjrUCzNC7tXw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "caa69b83-527b-4ee1-8cfc-f600789a098a", "AQAAAAEAACcQAAAAEAwkoK8bhrQgAUMc3ndwyEmUqNCNaney5YbcbdXTJkBoVd0mPwAWvHEGWMpKvSaMDg==" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ArticleId", "Comment", "EndDate", "ExpectedPrintDate", "ExpectedPrintDuration", "MachineId", "OrderTime", "Quantity", "Status" },
                values: new object[,]
                {
                    { new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"), new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), "", null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 54, 0, 0), 1, new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(4003), 20, 1 },
                    { new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"), new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), "", null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 5, 0, 0), 3, new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(3985), 100, 1 },
                    { new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"), new Guid("8919b7b3-86b2-4a83-8495-7eba2a58c358"), "", new DateTime(2023, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 50, 0, 0), 3, new DateTime(2023, 8, 7, 15, 14, 56, 745, DateTimeKind.Utc).AddTicks(3993), 1000, 2 },
                    { new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"), new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), "Test 2", null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 1, 30, 0, 0), 2, new DateTime(2023, 8, 7, 14, 49, 56, 745, DateTimeKind.Utc).AddTicks(4020), 40, 1 },
                    { new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"), new Guid("0c4b3ad4-545e-4805-b34d-2b5572d000a7"), "", null, new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 40, 0, 0), 4, new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(4026), 10000, 1 },
                    { new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"), new Guid("500f8057-d4bb-4839-9e15-bd260bbf532e"), "Test", null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), new TimeSpan(0, 0, 45, 0, 0), 2, new DateTime(2023, 8, 7, 14, 44, 56, 745, DateTimeKind.Utc).AddTicks(4015), 20, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("41e4eae1-eaac-4e34-bdf3-a6c19549dcdd"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98a6aa3d-8ef5-46aa-bb53-1eb9b886a786", "AQAAAAEAACcQAAAAED5fybgGDmh/uwccedr6kWJ8psyB7O6Zc4TORMKhBFQRalzBlxmTSU276vFVGpC+BA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6afbf121-61d4-42ca-a9c1-5ac694442d83"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f0037a82-6600-4336-a442-bdfe852665bd", "AQAAAAEAACcQAAAAECye9fPJGSJ4faR/JpcLmapblYUMvcKQ2dnEfS7m28hptiWRAXxKNIF8bpXUKqCIWQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ab1c2588-4ee2-408f-a302-fbddfd8ec1b8"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "39c7eb9f-6bcc-4f76-a377-f78edfce7368", "AQAAAAEAACcQAAAAEBznfPtwocPhIyEVh+zRVQMCeZqatgN30irOBYxhxq24WEQalEzpuk5zB7+auv82MA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e7065dbb-0c70-48da-902c-9f6f2536c505"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1abd7b69-062e-4d4e-84d7-430e14c93987", "AQAAAAEAACcQAAAAEEApF1U6C4QcEzx9OTtW2LCtSSGto0b/M6/pAjck2GCyRlpvaRjpcMuWtl8WtT5YKQ==" });
        }
    }
}
