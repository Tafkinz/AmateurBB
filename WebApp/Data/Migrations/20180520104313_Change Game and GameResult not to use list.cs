using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class ChangeGameandGameResultnottouselist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Standings_TeamId",
                table: "Standings");

            migrationBuilder.AddColumn<bool>(
                name: "IsHomeTeam",
                table: "GameTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Standings_TeamId",
                table: "Standings",
                column: "TeamId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Standings_TeamId",
                table: "Standings");

            migrationBuilder.DropColumn(
                name: "IsHomeTeam",
                table: "GameTeams");

            migrationBuilder.CreateIndex(
                name: "IX_Standings_TeamId",
                table: "Standings",
                column: "TeamId");
        }
    }
}
