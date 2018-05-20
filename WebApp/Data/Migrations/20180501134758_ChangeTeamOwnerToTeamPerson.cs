using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class ChangeTeamOwnerToTeamPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamOwnerId",
                table: "TeamOwners",
                newName: "TeamPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamPersonId",
                table: "TeamOwners",
                newName: "TeamOwnerId");
        }
    }
}
