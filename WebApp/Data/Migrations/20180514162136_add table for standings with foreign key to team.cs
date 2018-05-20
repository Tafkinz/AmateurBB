using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class addtableforstandingswithforeignkeytoteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Standings",
                columns: table => new
                {
                    StandingsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GamesPlayed = table.Column<int>(nullable: false),
                    Losses = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    TeamId = table.Column<long>(nullable: false),
                    Wins = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standings", x => x.StandingsId);
                    table.ForeignKey(
                        name: "FK_Standings_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_TeamId",
                table: "Standings",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Standings");
        }
    }
}
