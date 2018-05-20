using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class UserIDFKeyisstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_RefereeId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_RefereeId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "RefereeId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Games",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ApplicationUserId",
                table: "Contacts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_ApplicationUserId",
                table: "Contacts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_ApplicationUserId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_ApplicationUserId",
                table: "Contacts");

            migrationBuilder.AlterColumn<long>(
                name: "ApplicationUserId",
                table: "Games",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefereeId",
                table: "Games",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApplicationUserId",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_RefereeId",
                table: "Games",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ApplicationUserId1",
                table: "Contacts",
                column: "ApplicationUserId1");

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
        }
    }
}
