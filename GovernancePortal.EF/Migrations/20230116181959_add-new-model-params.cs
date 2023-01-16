using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addnewmodelparams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVotingEnded",
                table: "Votings");

            migrationBuilder.AddColumn<bool>(
                name: "HasVoted",
                table: "VotingUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResolutionStatus",
                table: "Votings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResolutionStatus",
                table: "Polls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVoted",
                table: "VotingUser");

            migrationBuilder.DropColumn(
                name: "ResolutionStatus",
                table: "Votings");

            migrationBuilder.DropColumn(
                name: "ResolutionStatus",
                table: "Polls");

            migrationBuilder.AddColumn<bool>(
                name: "IsVotingEnded",
                table: "Votings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
