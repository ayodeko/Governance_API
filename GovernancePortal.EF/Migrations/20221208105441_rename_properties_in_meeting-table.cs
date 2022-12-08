using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class rename_properties_in_meetingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretaryId",
                table: "Meetings",
                newName: "SecretaryUserId");

            migrationBuilder.RenameColumn(
                name: "ChairPersonId",
                table: "Meetings",
                newName: "ChairPersonUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretaryUserId",
                table: "Meetings",
                newName: "SecretaryId");

            migrationBuilder.RenameColumn(
                name: "ChairPersonUserId",
                table: "Meetings",
                newName: "ChairPersonId");
        }
    }
}
