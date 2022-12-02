﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    public partial class addmeetingnoticeagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingNotice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeetingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgendaText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mandate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signatory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendNoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingNotice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingNotice_Attachment_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Attachment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingNotice_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotice_MeetingId",
                table: "MeetingNotice",
                column: "MeetingId",
                unique: true,
                filter: "[MeetingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotice_SignatureId",
                table: "MeetingNotice",
                column: "SignatureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingNotice");
        }
    }
}