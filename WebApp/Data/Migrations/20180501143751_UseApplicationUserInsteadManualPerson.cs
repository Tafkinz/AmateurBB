using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class UseApplicationUserInsteadManualPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_People_PersonId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamOwners_People_PersonId",
                table: "TeamOwners");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropIndex(
                name: "IX_TeamOwners_PersonId",
                table: "TeamOwners");

            migrationBuilder.DropIndex(
                name: "IX_Games_PersonId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "TeamOwners");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Games",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Contacts",
                newName: "ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TeamOwners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefereeId",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "PersonTypeId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TeamOwners_ApplicationUserId",
                table: "TeamOwners",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RefereeId",
                table: "Games",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ApplicationUserId1",
                table: "Contacts",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonTypeId",
                table: "AspNetUsers",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonTypes_PersonTypeId",
                table: "AspNetUsers",
                column: "PersonTypeId",
                principalTable: "PersonTypes",
                principalColumn: "PersonTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_ApplicationUserId1",
                table: "Contacts",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_RefereeId",
                table: "Games",
                column: "RefereeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamOwners_AspNetUsers_ApplicationUserId",
                table: "TeamOwners",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonTypes_PersonTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_RefereeId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamOwners_AspNetUsers_ApplicationUserId",
                table: "TeamOwners");

            migrationBuilder.DropIndex(
                name: "IX_TeamOwners_ApplicationUserId",
                table: "TeamOwners");

            migrationBuilder.DropIndex(
                name: "IX_Games_RefereeId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TeamOwners");

            migrationBuilder.DropColumn(
                name: "RefereeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonTypeId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Games",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Contacts",
                newName: "PersonId");

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "TeamOwners",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 128, nullable: true),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    Password = table.Column<string>(maxLength: 12560, nullable: true),
                    PersonTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "PersonTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamOwners_PersonId",
                table: "TeamOwners",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PersonId",
                table: "Games",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonTypeId",
                table: "People",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_People_PersonId",
                table: "Games",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamOwners_People_PersonId",
                table: "TeamOwners",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
