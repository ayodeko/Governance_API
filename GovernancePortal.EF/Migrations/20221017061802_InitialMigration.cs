using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingPacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    Downloadable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingPacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChairPersonId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretaryId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Medium = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    MeetingPackId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingPackItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingPackId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MeetingAgendaItemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresenterUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionRequired = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingPackItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingPackItem_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingPackItem_MeetingPacks_MeetingPackId",
                        column: x => x.MeetingPackId,
                        principalTable: "MeetingPacks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guest_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingAgendaItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MeetingAgendaItemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNumbered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAgendaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAgendaItems_MeetingAgendaItems_MeetingAgendaItemId",
                        column: x => x.MeetingAgendaItemId,
                        principalTable: "MeetingAgendaItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingAgendaItems_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingAttendances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneratedCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAttendances_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Minutes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgendaItemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinuteText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresenterUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignerUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minutes_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Minutes_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendingUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MeetingAttendanceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendeePosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendingUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendingUsers_MeetingAttendances_MeetingAttendanceId",
                        column: x => x.MeetingAttendanceId,
                        principalTable: "MeetingAttendances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendingUsers_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingPackItemUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendingUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CoCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RestrictedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InterestTagUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingPackItemUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingPackItemUsers_AttendingUsers_AttendingUserId",
                        column: x => x.AttendingUserId,
                        principalTable: "AttendingUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingPackItemUsers_MeetingPackItem_CoCreatorId",
                        column: x => x.CoCreatorId,
                        principalTable: "MeetingPackItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingPackItemUsers_MeetingPackItem_InterestTagUserId",
                        column: x => x.InterestTagUserId,
                        principalTable: "MeetingPackItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingPackItemUsers_MeetingPackItem_RestrictedUserId",
                        column: x => x.RestrictedUserId,
                        principalTable: "MeetingPackItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendingUsers_MeetingAttendanceId",
                table: "AttendingUsers",
                column: "MeetingAttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendingUsers_MeetingId",
                table: "AttendingUsers",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Guest_MeetingId",
                table: "Guest",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaItems_MeetingAgendaItemId",
                table: "MeetingAgendaItems",
                column: "MeetingAgendaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAgendaItems_MeetingId",
                table: "MeetingAgendaItems",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAttendances_MeetingId",
                table: "MeetingAttendances",
                column: "MeetingId",
                unique: true,
                filter: "[MeetingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItem_AttachmentId",
                table: "MeetingPackItem",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItem_MeetingPackId",
                table: "MeetingPackItem",
                column: "MeetingPackId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_AttendingUserId",
                table: "MeetingPackItemUsers",
                column: "AttendingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_CoCreatorId",
                table: "MeetingPackItemUsers",
                column: "CoCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_InterestTagUserId",
                table: "MeetingPackItemUsers",
                column: "InterestTagUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingPackItemUsers_RestrictedUserId",
                table: "MeetingPackItemUsers",
                column: "RestrictedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_AttachmentId",
                table: "Minutes",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Minutes_MeetingId",
                table: "Minutes",
                column: "MeetingId",
                unique: true,
                filter: "[MeetingId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "MeetingAgendaItems");

            migrationBuilder.DropTable(
                name: "MeetingPackItemUsers");

            migrationBuilder.DropTable(
                name: "Minutes");

            migrationBuilder.DropTable(
                name: "AttendingUsers");

            migrationBuilder.DropTable(
                name: "MeetingPackItem");

            migrationBuilder.DropTable(
                name: "MeetingAttendances");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "MeetingPacks");

            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}
