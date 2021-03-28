using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokerSNTS.Infra.Data.Migrations
{
    public partial class RemovingTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Regulations");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "RankingPunctuations");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Ranking");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "PlayersRounds");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Players");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Rounds",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Regulations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "RankingPunctuations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Ranking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "PlayersRounds",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Players",
                type: "datetime2",
                nullable: true);
        }
    }
}
