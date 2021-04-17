using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerSNTS.Infra.Data.Migrations
{
    public partial class UpdatePunctuationToPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingPunctuations");

            migrationBuilder.DropTable(
                name: "RoundsPunctuations");

            migrationBuilder.CreateTable(
                name: "RankingPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    Point = table.Column<short>(type: "smallint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoundsPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    Point = table.Column<short>(type: "smallint", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundsPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundsPoints_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundsPoints_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoundsPoints_PlayerId",
                table: "RoundsPoints",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundsPoints_RoundId",
                table: "RoundsPoints",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingPoints");

            migrationBuilder.DropTable(
                name: "RoundsPoints");

            migrationBuilder.CreateTable(
                name: "RankingPunctuations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    Punctuation = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingPunctuations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoundsPunctuations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    Punctuation = table.Column<short>(type: "smallint", nullable: false),
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundsPunctuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoundsPunctuations_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoundsPunctuations_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoundsPunctuations_PlayerId",
                table: "RoundsPunctuations",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoundsPunctuations_RoundId",
                table: "RoundsPunctuations",
                column: "RoundId");
        }
    }
}
