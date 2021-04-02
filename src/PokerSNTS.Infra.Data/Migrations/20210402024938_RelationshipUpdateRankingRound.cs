using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerSNTS.Infra.Data.Migrations
{
    public partial class RelationshipUpdateRankingRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankingRounds");

            migrationBuilder.AddColumn<Guid>(
                name: "RankingId",
                table: "Rounds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_RankingId",
                table: "Rounds",
                column: "RankingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rounds_Ranking_RankingId",
                table: "Rounds",
                column: "RankingId",
                principalTable: "Ranking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rounds_Ranking_RankingId",
                table: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Rounds_RankingId",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "RankingId",
                table: "Rounds");

            migrationBuilder.CreateTable(
                name: "RankingRounds",
                columns: table => new
                {
                    RankingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingRounds", x => new { x.RankingId, x.RoundsId });
                    table.ForeignKey(
                        name: "FK_RankingRounds_Ranking_RankingId",
                        column: x => x.RankingId,
                        principalTable: "Ranking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RankingRounds_Rounds_RoundsId",
                        column: x => x.RoundsId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RankingRounds_RoundsId",
                table: "RankingRounds",
                column: "RoundsId");
        }
    }
}
