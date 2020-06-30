using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class authorProjectName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "authorName",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "projectName",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authorName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "projectName",
                table: "Tickets");
        }
    }
}
