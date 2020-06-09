using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class justKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "UserModels");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "UserModels",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModels",
                table: "UserModels",
                column: "key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModels",
                table: "UserModels");

            migrationBuilder.AlterColumn<string>(
                name: "key",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "UserModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
