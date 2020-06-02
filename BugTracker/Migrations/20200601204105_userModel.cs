using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class userModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userAssignedProjectUserModelId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userCreatedProjectUserModelId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    numCompletedTickets = table.Column<int>(nullable: false),
                    numCompletedProjects = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserModelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserModelId",
                table: "Tickets",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_userAssignedProjectUserModelId",
                table: "Projects",
                column: "userAssignedProjectUserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_userCreatedProjectUserModelId",
                table: "Projects",
                column: "userCreatedProjectUserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_userAssignedProjectUserModelId",
                table: "Projects",
                column: "userAssignedProjectUserModelId",
                principalTable: "Users",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_userCreatedProjectUserModelId",
                table: "Projects",
                column: "userCreatedProjectUserModelId",
                principalTable: "Users",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserModelId",
                table: "Tickets",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_userAssignedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_userCreatedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserModelId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserModelId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Projects_userAssignedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_userCreatedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "userAssignedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "userCreatedProjectUserModelId",
                table: "Projects");
        }
    }
}
