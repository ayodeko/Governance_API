using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addIsLinkedToMeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLinkedToMeeting",
                table: "Votings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLinkedToMeeting",
                table: "Polls",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLinkedToMeeting",
                table: "Votings");

            migrationBuilder.DropColumn(
                name: "IsLinkedToMeeting",
                table: "Polls");
        }
    }
}
