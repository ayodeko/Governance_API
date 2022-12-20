using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addnewresolutioncolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meeting_Resolutions_MeetingId",
                table: "Meeting_Resolutions");

            migrationBuilder.DropColumn(
                name: "DocumentStatus",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "HasExpiryDate",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "Highlight",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "OtherDetails",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "ReferenceDate",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "ReferenceDescription",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "TaskAttachment");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "TaskAttachment");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymousVote",
                table: "Polls",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnonymousVote",
                table: "Polls");

            migrationBuilder.AddColumn<string>(
                name: "DocumentStatus",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasExpiryDate",
                table: "TaskAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Highlight",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TaskAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherDetails",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReferenceDate",
                table: "TaskAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceDescription",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "TaskAttachment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "TaskAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "TaskAttachment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meeting_Resolutions_MeetingId",
                table: "Meeting_Resolutions",
                column: "MeetingId",
                unique: true);
        }
    }
}
