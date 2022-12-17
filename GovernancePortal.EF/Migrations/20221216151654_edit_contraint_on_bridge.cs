using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class edit_contraint_on_bridge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Resolutions_MeetingId",
                table: "Meeting_Resolutions",
                column: "MeetingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Resolutions_ResolutionId",
                table: "Meeting_Resolutions",
                column: "ResolutionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meeting_Resolutions_MeetingId",
                table: "Meeting_Resolutions");

            migrationBuilder.DropIndex(
                name: "IX_Meeting_Resolutions_ResolutionId",
                table: "Meeting_Resolutions");
        }
    }
}
