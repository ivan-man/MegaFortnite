using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaFortnite.DataAccess.Migrations
{
    public partial class corrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LobbyPassword",
                schema: "public",
                table: "Sessions");

            migrationBuilder.AlterColumn<string>(
                name: "LobbyKey",
                schema: "public",
                table: "Sessions",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WinRate",
                schema: "public",
                table: "Profiles",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9179));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9503));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 3, 8, 21, 17, 3, 971, DateTimeKind.Utc).AddTicks(9506));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9393), 0m });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9399), 0m });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 8, 21, 17, 3, 972, DateTimeKind.Utc).AddTicks(9400), 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LobbyKey",
                schema: "public",
                table: "Sessions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LobbyPassword",
                schema: "public",
                table: "Sessions",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WinRate",
                schema: "public",
                table: "Profiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(7487));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(8429));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(8435));

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6521), 0 });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6531), 0 });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "WinRate" },
                values: new object[] { new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6533), 0 });
        }
    }
}
