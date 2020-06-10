using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class virtualAuthoredProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserModelkey",
                table: "Projects",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
