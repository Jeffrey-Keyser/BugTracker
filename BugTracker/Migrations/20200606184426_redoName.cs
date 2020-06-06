using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class redoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserModel");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel",
                column: "UserModelId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModel",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserModel",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserModelId");

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
    }
}
