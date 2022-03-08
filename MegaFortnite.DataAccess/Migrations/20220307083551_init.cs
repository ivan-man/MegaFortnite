using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MegaFortnite.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NickName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    WinRate = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "public",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LobbyKey = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    LobbyPassword = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Profiles_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "public",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "public",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameProfileId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => new { x.SessionId, x.GameProfileId });
                    table.ForeignKey(
                        name: "FK_Results_Profiles_GameProfileId",
                        column: x => x.GameProfileId,
                        principalSchema: "public",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "public",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Customers",
                columns: new[] { "Id", "Created", "Email", "FirstName", "LastName", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(7487), "test1@test.com", "Customer_1", null, "", null },
                    { 2, new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(8429), "test2@test.com", "Customer_2", null, "", null },
                    { 3, new DateTime(2022, 3, 7, 8, 35, 51, 364, DateTimeKind.Utc).AddTicks(8435), "test3@test.com", "Customer_3", null, "", null }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Profiles",
                columns: new[] { "Id", "Created", "CustomerId", "NickName", "Rate", "Updated", "WinRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6521), 1, "xXx_predator_xXx", 0, null, 0 },
                    { 2, new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6531), 1, "HArU6ATOP", 0, null, 0 },
                    { 3, new DateTime(2022, 3, 7, 8, 35, 51, 366, DateTimeKind.Utc).AddTicks(6533), 1, "4TO_C_E6AJIOM", 0, null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                schema: "public",
                table: "Customers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Phone",
                schema: "public",
                table: "Customers",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CustomerId",
                schema: "public",
                table: "Profiles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_GameProfileId",
                schema: "public",
                table: "Results",
                column: "GameProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_SessionId",
                schema: "public",
                table: "Results",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_OwnerId",
                schema: "public",
                table: "Sessions",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "public");
        }
    }
}
