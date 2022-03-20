using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MegaFortnite.DataAccess.Migrations
{
    public partial class reinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NickName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    WinRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LobbyKey = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    OwnerId1 = table.Column<int>(type: "integer", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Profiles_OwnerId1",
                        column: x => x.OwnerId1,
                        principalSchema: "public",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "Profiles",
                columns: new[] { "Id", "Created", "CustomerId", "NickName", "Rate", "Updated", "WinRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 20, 22, 11, 37, 571, DateTimeKind.Utc).AddTicks(2788), new Guid("a6b3ee91-1be7-4eab-a15b-7bffc8b94bff"), "xXx_predator_xXx", 0, null, 0m },
                    { 2, new DateTime(2022, 3, 20, 22, 11, 37, 571, DateTimeKind.Utc).AddTicks(3184), new Guid("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfa"), "HArU6ATOP", 0, null, 0m },
                    { 3, new DateTime(2022, 3, 20, 22, 11, 37, 571, DateTimeKind.Utc).AddTicks(3188), new Guid("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfb"), "4TO_C_E6AJIOM", 0, null, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CustomerId",
                schema: "public",
                table: "Profiles",
                column: "CustomerId",
                unique: true);

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
                name: "IX_Sessions_OwnerId1",
                schema: "public",
                table: "Sessions",
                column: "OwnerId1");
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
        }
    }
}
