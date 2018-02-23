using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LoadDb.Migrations
{
    public partial class languages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Language_GovernmentBodies_GovernmentBodyId",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameIndex(
                name: "IX_Language_GovernmentBodyId",
                table: "Languages",
                newName: "IX_Languages_GovernmentBodyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_GovernmentBodies_GovernmentBodyId",
                table: "Languages",
                column: "GovernmentBodyId",
                principalTable: "GovernmentBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_GovernmentBodies_GovernmentBodyId",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_GovernmentBodyId",
                table: "Language",
                newName: "IX_Language_GovernmentBodyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_GovernmentBodies_GovernmentBodyId",
                table: "Language",
                column: "GovernmentBodyId",
                principalTable: "GovernmentBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
