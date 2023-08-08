using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Infrastructure.Migrations
{
    public partial class AddOrdersAndOneMoreClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Email", "IsActive", "MerchantId", "Name", "PhoneNumber" },
                values: new object[] { new Guid("46b7d975-1579-4dad-bdc6-f9dbd0232eab"), "clienttest@email.com", true, 2, "Test test", "+05656864545" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 20, 17, 48, 939, DateTimeKind.Utc).AddTicks(7725));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 19, 52, 48, 939, DateTimeKind.Utc).AddTicks(7743));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 19, 42, 48, 939, DateTimeKind.Utc).AddTicks(7749));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 19, 47, 48, 939, DateTimeKind.Utc).AddTicks(7739));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("46b7d975-1579-4dad-bdc6-f9dbd0232eab"));

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

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("17e2de12-142f-47c7-9af8-3b138eb9cfbf"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(4003));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("54b5df45-d161-45c6-a6ba-f0c37d5667b6"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(3985));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("5a167a8c-5136-4c74-90b2-2c958ae54b20"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 15, 14, 56, 745, DateTimeKind.Utc).AddTicks(3993));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("8d93617f-ec76-4831-8dd6-73e78eb821b4"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 14, 49, 56, 745, DateTimeKind.Utc).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("b572ce54-5707-426f-b8a9-2d765bc415e4"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 14, 39, 56, 745, DateTimeKind.Utc).AddTicks(4026));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("ca5b12c6-f0cb-41a9-83db-be95e5509a43"),
                column: "OrderTime",
                value: new DateTime(2023, 8, 7, 14, 44, 56, 745, DateTimeKind.Utc).AddTicks(4015));
        }
    }
}
