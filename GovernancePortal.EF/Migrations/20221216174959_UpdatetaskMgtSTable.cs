using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class UpdatetaskMgtSTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TaskParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "TaskParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TaskParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "TaskParticipant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskParticipant",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TaskParticipant");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TaskParticipant");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TaskParticipant");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "TaskParticipant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskParticipant");
        }
    }
}
