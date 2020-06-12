﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class removeVirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserModels_UserModelkey",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserModelkey",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserModelkey",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserModelkey",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserModelkey",
                table: "Projects",
                column: "UserModelkey");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserModels_UserModelkey",
                table: "Projects",
                column: "UserModelkey",
                principalTable: "UserModels",
                principalColumn: "key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}