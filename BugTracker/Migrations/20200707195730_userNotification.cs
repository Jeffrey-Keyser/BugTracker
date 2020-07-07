using Microsoft.EntityFrameworkCore.Migrations;

namespace BugTracker.Migrations
{
    public partial class userNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    AuthorId = table.Column<string>(nullable: false),
                    AffectedId = table.Column<string>(nullable: false),
                    NotificationAuthorId = table.Column<string>(nullable: true),
                    AffectedUserId = table.Column<string>(nullable: true),
                    NotificationMessage = table.Column<string>(nullable: true),
                    NotificationSeen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => new { x.AffectedId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_AffectedUserId",
                        column: x => x.AffectedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNotifications_AspNetUsers_NotificationAuthorId",
                        column: x => x.NotificationAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_AffectedUserId",
                table: "UserNotifications",
                column: "AffectedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_NotificationAuthorId",
                table: "UserNotifications",
                column: "NotificationAuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotifications");
        }
    }
}
