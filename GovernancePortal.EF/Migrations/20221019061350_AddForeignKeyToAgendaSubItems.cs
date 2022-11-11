using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class AddForeignKeyToAgendaSubItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAgendaItems_MeetingAgendaItems_MeetingAgendaItemId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropIndex(
                name: "IX_MeetingAgendaItems_MeetingAgendaItemId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropColumn(
                name: "MeetingAgendaItemId",
                table: "MeetingAgendaItems");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "MeetingAgendaItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaItems_ParentId",
                table: "MeetingAgendaItems",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAgendaItems_MeetingAgendaItems_ParentId",
                table: "MeetingAgendaItems",
                column: "ParentId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAgendaItems_MeetingAgendaItems_ParentId",
                table: "MeetingAgendaItems");

            migrationBuilder.DropIndex(
                name: "IX_MeetingAgendaItems_ParentId",
                table: "MeetingAgendaItems");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "MeetingAgendaItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeetingAgendaItemId",
                table: "MeetingAgendaItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaItems_MeetingAgendaItemId",
                table: "MeetingAgendaItems",
                column: "MeetingAgendaItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAgendaItems_MeetingAgendaItems_MeetingAgendaItemId",
                table: "MeetingAgendaItems",
                column: "MeetingAgendaItemId",
                principalTable: "MeetingAgendaItems",
                principalColumn: "Id");
        }
    }
}
