using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class add_new_meeting_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StandAloneMinute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgendaItemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MinuteText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    AttachmentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandAloneMinute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandAloneMinute_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StandAloneMinute_MeetingAgendaItems_AgendaItemId",
                        column: x => x.AgendaItemId,
                        principalTable: "MeetingAgendaItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StandAloneMinute_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StandAloneMinute_AgendaItemId",
                table: "StandAloneMinute",
                column: "AgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StandAloneMinute_AttachmentId",
                table: "StandAloneMinute",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StandAloneMinute_MeetingId",
                table: "StandAloneMinute",
                column: "MeetingId",
                unique: true,
                filter: "[MeetingId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandAloneMinute");
        }
    }
}
