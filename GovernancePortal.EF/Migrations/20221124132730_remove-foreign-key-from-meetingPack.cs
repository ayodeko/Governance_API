using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class removeforeignkeyfrommeetingPack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_CoCreatorId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_InterestTagUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_RestrictedUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.AddColumn<string>(
                name: "MeetingPackItemId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingPackItemId1",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingPackItemId2",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId1",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId2",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId2");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId1",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId1",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId2",
                table: "MeetingPackItemUsers",
                column: "MeetingPackItemId2",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId1",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_MeetingPackItemId2",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId1",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItemUsers_MeetingPackItemId2",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "MeetingPackItemId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "MeetingPackItemId1",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "MeetingPackItemId2",
                table: "MeetingPackItemUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_CoCreatorId",
                table: "MeetingPackItemUsers",
                column: "CoCreatorId",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_InterestTagUserId",
                table: "MeetingPackItemUsers",
                column: "InterestTagUserId",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingPackItem_RestrictedUserId",
                table: "MeetingPackItemUsers",
                column: "RestrictedUserId",
                principalTable: "MeetingPackItem",
                principalColumn: "Id");
        }
    }
}
