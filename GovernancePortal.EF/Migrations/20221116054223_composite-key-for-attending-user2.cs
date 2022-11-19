using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class compositekeyforattendinguser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendingUsers_Meetings_MeetingId",
                table: "AttendingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_AttendingUsers_AttendingUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItemUsers_AttendingUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendingUsers",
                table: "AttendingUsers");

            migrationBuilder.DropIndex(
                name: "IX_AttendingUsers_MeetingId",
                table: "AttendingUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AttendingUserId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendingUserMeetingId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendingUserUserId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AttendingUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingId",
                table: "AttendingUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AttendingUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendingUsers",
                table: "AttendingUsers",
                columns: new[] { "MeetingId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_AttendingUserMeetingId_AttendingUserUserId",
                table: "MeetingPackItemUsers",
                columns: new[] { "AttendingUserMeetingId", "AttendingUserUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttendingUsers_Meetings_MeetingId",
                table: "AttendingUsers",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_AttendingUsers_AttendingUserMeetingId_AttendingUserUserId",
                table: "MeetingPackItemUsers",
                columns: new[] { "AttendingUserMeetingId", "AttendingUserUserId" },
                principalTable: "AttendingUsers",
                principalColumns: new[] { "MeetingId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendingUsers_Meetings_MeetingId",
                table: "AttendingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingPackItemUsers_AttendingUsers_AttendingUserMeetingId_AttendingUserUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropIndex(
                name: "IX_MeetingPackItemUsers_AttendingUserMeetingId_AttendingUserUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttendingUsers",
                table: "AttendingUsers");

            migrationBuilder.DropColumn(
                name: "AttendingUserMeetingId",
                table: "MeetingPackItemUsers");

            migrationBuilder.DropColumn(
                name: "AttendingUserUserId",
                table: "MeetingPackItemUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AttendingUserId",
                table: "MeetingPackItemUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AttendingUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AttendingUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingId",
                table: "AttendingUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttendingUsers",
                table: "AttendingUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_AttendingUserId",
                table: "MeetingPackItemUsers",
                column: "AttendingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendingUsers_MeetingId",
                table: "AttendingUsers",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendingUsers_Meetings_MeetingId",
                table: "AttendingUsers",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingPackItemUsers_AttendingUsers_AttendingUserId",
                table: "MeetingPackItemUsers",
                column: "AttendingUserId",
                principalTable: "AttendingUsers",
                principalColumn: "Id");
        }
    }
}
