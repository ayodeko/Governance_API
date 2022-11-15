using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addmeetingnoticeasdbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoticeMeeting_Attachment_SignatureId",
                table: "NoticeMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_NoticeMeeting_Meetings_MeetingId",
                table: "NoticeMeeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoticeMeeting",
                table: "NoticeMeeting");

            migrationBuilder.RenameTable(
                name: "NoticeMeeting",
                newName: "Notices");

            migrationBuilder.RenameIndex(
                name: "IX_NoticeMeeting_SignatureId",
                table: "Notices",
                newName: "IX_Notices_SignatureId");

            migrationBuilder.RenameIndex(
                name: "IX_NoticeMeeting_MeetingId",
                table: "Notices",
                newName: "IX_Notices_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notices",
                table: "Notices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Attachment_SignatureId",
                table: "Notices",
                column: "SignatureId",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_Meetings_MeetingId",
                table: "Notices",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Attachment_SignatureId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_Meetings_MeetingId",
                table: "Notices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notices",
                table: "Notices");

            migrationBuilder.RenameTable(
                name: "Notices",
                newName: "NoticeMeeting");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_SignatureId",
                table: "NoticeMeeting",
                newName: "IX_NoticeMeeting_SignatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_MeetingId",
                table: "NoticeMeeting",
                newName: "IX_NoticeMeeting_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoticeMeeting",
                table: "NoticeMeeting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoticeMeeting_Attachment_SignatureId",
                table: "NoticeMeeting",
                column: "SignatureId",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NoticeMeeting_Meetings_MeetingId",
                table: "NoticeMeeting",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");
        }
    }
}
