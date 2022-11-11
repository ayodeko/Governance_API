using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class add_extra_fields_to_meeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAttendanceTaken",
                table: "Meetings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMinutesPublished",
                table: "Meetings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAttendanceTaken",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsMinutesPublished",
                table: "Meetings");
        }
    }
}
