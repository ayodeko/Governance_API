using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class UpdateMinuteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Minutes_MeetingId",
                table: "Minutes");

            migrationBuilder.DropColumn(
                name: "SignerUserId",
                table: "Minutes");

            migrationBuilder.AlterColumn<string>(
                name: "AgendaItemId",
                table: "Minutes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Minutes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMinuteApproved",
                table: "AttendingUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_AgendaItemId",
                table: "Minutes",
                column: "AgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_MeetingId",
                table: "Minutes",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Minutes_MeetingAgendaItems_AgendaItemId",
                table: "Minutes",
                column: "AgendaItemId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Minutes_MeetingAgendaItems_AgendaItemId",
                table: "Minutes");

            migrationBuilder.DropIndex(
                name: "IX_Minutes_AgendaItemId",
                table: "Minutes");

            migrationBuilder.DropIndex(
                name: "IX_Minutes_MeetingId",
                table: "Minutes");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Minutes");

            migrationBuilder.DropColumn(
                name: "IsMinuteApproved",
                table: "AttendingUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AgendaItemId",
                table: "Minutes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignerUserId",
                table: "Minutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_MeetingId",
                table: "Minutes",
                column: "MeetingId",
                unique: true,
                filter: "[MeetingId] IS NOT NULL");
        }
    }
}
