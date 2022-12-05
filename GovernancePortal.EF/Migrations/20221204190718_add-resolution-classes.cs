using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addresolutionclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "Notices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "MeetingPackItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "MeetingAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "MeetingAgendaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "AttendingUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelStatus",
                table: "Attachment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "MeetingAttendances");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "ModelStatus",
                table: "Attachment");
        }
    }
}
