using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MessageApp.Infrastructure.Migrations
{
    public partial class add_seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Initial user" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "John Smith" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "ReadDate", "ReceiverId", "SendDate", "SenderId" },
                values: new object[] { 1, "message 1", null, 1, new DateTime(2021, 2, 15, 17, 34, 56, 684, DateTimeKind.Local).AddTicks(6443), 2 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "ReadDate", "ReceiverId", "SendDate", "SenderId" },
                values: new object[] { 2, "message 2", null, 1, new DateTime(2021, 2, 16, 17, 34, 56, 686, DateTimeKind.Local).AddTicks(1148), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
