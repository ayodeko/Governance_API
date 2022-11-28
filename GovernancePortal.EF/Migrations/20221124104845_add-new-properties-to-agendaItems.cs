using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addnewpropertiestoagendaItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingIdHolder",
                table: "MeetingPackItemUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActionRequired",
                table: "MeetingAgendaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentId",
                table: "MeetingAgendaItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MeetingAgendaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "MeetingAgendaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PresenterUserId",
                table: "MeetingAgendaItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaItems_AttachmentId",
                table: "MeetingAgendaItems",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAgendaItems_Attachment_AttachmentId",
                table: "MeetingAgendaItems",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_CoCreatorId",
                table: "MeetingPackItemUsers",
                column: "CoCreatorId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_InterestTagUserId",
                table: "MeetingPackItemUsers",
                column: "InterestTagUserId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_RestrictedUserId",
                table: "MeetingPackItemUsers",
                column: "RestrictedUserId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAgendaItems_Attachment_AttachmentId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_CoCreatorId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_InterestTagUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_MeetingAgendaItems_RestrictedUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingAgendaItems_AttachmentId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "MeetingIdHolder",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "ActionRequired",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "PresenterUserId",
                table: "MeetingAgendaItems");
        }
    }
}
