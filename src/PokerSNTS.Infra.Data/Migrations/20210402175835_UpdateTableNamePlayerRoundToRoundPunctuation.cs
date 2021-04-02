using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerSNTS.Infra.Data.Migrations
{
    public partial class UpdateTableNamePlayerRoundToRoundPunctuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersRounds");

            migrationBuilder.CreateTable(
                name: "RoundsPunctuations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<short>(type: "smallint", nullable: false),
                    Punctuation = table.Column<short>(type: "smallint", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoundsPunctuations");

            migrationBuilder.CreateTable(
                name: "PlayersRounds",
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
                    table.PrimaryKey("PK_PlayersRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayersRounds_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayersRounds_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayersRounds_PlayerId",
                table: "PlayersRounds",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersRounds_RoundId",
                table: "PlayersRounds",
                column: "RoundId");
        }
    }
}
