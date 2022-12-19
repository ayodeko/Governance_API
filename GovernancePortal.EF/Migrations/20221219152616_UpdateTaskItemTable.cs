using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class UpdateTaskItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_TaskItem_TaskItemId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_TaskItemId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "Attachment");

            migrationBuilder.CreateTable(
                name: "TaskAttachment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Highlight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasExpiryDate = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskItemId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachment_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachment_TaskItemId",
                table: "TaskAttachment",
                column: "TaskItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAttachment");

            migrationBuilder.AddColumn<string>(
                name: "TaskItemId",
                table: "Attachment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TaskItemId",
                table: "Attachment",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_TaskItem_TaskItemId",
                table: "Attachment",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id");
        }
    }
}
