using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class Removegameresultstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTeams_GameResults_GameResultId",
                table: "GameTeams");

            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropIndex(
                name: "IX_GameTeams_GameResultId",
                table: "GameTeams");

            migrationBuilder.DropColumn(
                name: "GameResultId",
                table: "GameTeams");

            migrationBuilder.RenameColumn(
                name: "IsHomeTeam",
                table: "GameTeams",
                newName: "ResultConfirmed");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmedTs",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "RefereeConfirmed",
                table: "Games",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ContactValue",
                table: "Contacts",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmedTs",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RefereeConfirmed",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "ResultConfirmed",
                table: "GameTeams",
                newName: "IsHomeTeam");

            migrationBuilder.AddColumn<long>(
                name: "GameResultId",
                table: "GameTeams",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "ContactValue",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    GameResultId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayTeamConfirmed = table.Column<bool>(nullable: false),
                    ConfirmedTs = table.Column<DateTime>(nullable: false),
                    HomeTeamConfirmed = table.Column<bool>(nullable: false),
                    RefereeConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.GameResultId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTeams_GameResultId",
                table: "GameTeams",
                column: "GameResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTeams_GameResults_GameResultId",
                table: "GameTeams",
                column: "GameResultId",
                principalTable: "GameResults",
                principalColumn: "GameResultId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
