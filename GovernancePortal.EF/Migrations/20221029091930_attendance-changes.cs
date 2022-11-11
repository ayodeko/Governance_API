using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class attendancechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresenterUserId",
                table: "Minutes");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MeetingAttendances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "MeetingAttendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "MeetingAttendances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MeetingAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "AttendingUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AttendingUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AttendingUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AttendingUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AttendingUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MeetingAttendances");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "MeetingAttendances");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "MeetingAttendances");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MeetingAttendances");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AttendingUsers");

            migrationBuilder.AddColumn<string>(
                name: "PresenterUserId",
                table: "Minutes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
