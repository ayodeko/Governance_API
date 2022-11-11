using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class extramodelchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MeetingId",
                table: "MeetingPackItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MeetingPackItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "MeetingPackItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "MeetingPackItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MeetingPackItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "MeetingAgendaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MeetingAgendaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "MeetingAgendaItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "MeetingAgendaItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MeetingAgendaItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItem_MeetingId",
                table: "MeetingPackItem",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItem_Meetings_MeetingId",
                table: "MeetingPackItem",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItem_Meetings_MeetingId",
                table: "MeetingPackItem");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItem_MeetingId",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MeetingPackItem");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MeetingAgendaItems");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingId",
                table: "MeetingPackItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
