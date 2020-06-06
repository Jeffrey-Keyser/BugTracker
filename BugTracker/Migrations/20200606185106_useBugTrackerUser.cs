using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class useBugTrackerUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserModel_userAssignedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserModel_userCreatedProjectUserModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_UserModel_UserModelId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "UserModel");

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

            migrationBuilder.AddColumn<string>(
                name: "BugTrackerUserId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userAssignedProjectId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userCreatedProjectId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "numCompletedProjects",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "numCompletedTickets",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BugTrackerUserId",
                table: "Tickets",
                column: "BugTrackerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_userAssignedProjectId",
                table: "Projects",
                column: "userAssignedProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_userCreatedProjectId",
                table: "Projects",
                column: "userCreatedProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_userAssignedProjectId",
                table: "Projects",
                column: "userAssignedProjectId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_userCreatedProjectId",
                table: "Projects",
                column: "userCreatedProjectId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_BugTrackerUserId",
                table: "Tickets",
                column: "BugTrackerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_userAssignedProjectId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_userCreatedProjectId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_BugTrackerUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_BugTrackerUserId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Projects_userAssignedProjectId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_userCreatedProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BugTrackerUserId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "userAssignedProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "userCreatedProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "numCompletedProjects",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "numCompletedTickets",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userAssignedProjectUserModelId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "userCreatedProjectUserModelId",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    UserModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    numCompletedProjects = table.Column<int>(type: "int", nullable: false),
                    numCompletedTickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.UserModelId);
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
                name: "FK_Projects_UserModel_userAssignedProjectUserModelId",
                table: "Projects",
                column: "userAssignedProjectUserModelId",
                principalTable: "UserModel",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserModel_userCreatedProjectUserModelId",
                table: "Projects",
                column: "userCreatedProjectUserModelId",
                principalTable: "UserModel",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_UserModel_UserModelId",
                table: "Tickets",
                column: "UserModelId",
                principalTable: "UserModel",
                principalColumn: "UserModelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
