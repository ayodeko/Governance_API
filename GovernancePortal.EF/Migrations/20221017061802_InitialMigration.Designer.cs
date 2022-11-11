﻿// <auto-generated />
using System;
using GovernancePortal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    [DbContext(typeof(PortalContext))]
    [Migration("20221017061802_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, (int) 1L, 1);

            modelBuilder.Entity("GovernancePortal.Core.General.Attachment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasExpiryDate")
                        .HasColumnType("bit");

                    b.Property<string>("Highlight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("OtherDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReferenceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferenceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("VersionNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.AttendingUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AttendeePosition")
                        .HasColumnType("int");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingAttendanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingAttendanceId");

                    b.HasIndex("MeetingId");

                    b.ToTable("AttendingUsers");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Guest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId");

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChairPersonId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("Medium")
                        .HasColumnType("int");

                    b.Property<string>("MeetingPackId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecretaryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsNumbered")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingAgendaItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingAgendaItemId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingAgendaItems");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAttendance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneratedCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId")
                        .IsUnique()
                        .HasFilter("[MeetingId] IS NOT NULL");

                    b.ToTable("MeetingAttendances");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPack", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Downloadable")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MeetingPacks");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ActionRequired")
                        .HasColumnType("int");

                    b.Property<string>("AttachmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Duration")
                        .HasColumnType("datetime2");

                    b.Property<string>("MeetingAgendaItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingPackId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PresenterUserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("MeetingPackId");

                    b.ToTable("MeetingPackItem");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItemUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttendingUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CoCreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("InterestTagUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RestrictedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AttendingUserId");

                    b.HasIndex("CoCreatorId");

                    b.HasIndex("InterestTagUserId");

                    b.HasIndex("RestrictedUserId");

                    b.ToTable("MeetingPackItemUsers");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Minutes", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgendaItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttachmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MinuteText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresenterUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignerUserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("MeetingId")
                        .IsUnique()
                        .HasFilter("[MeetingId] IS NOT NULL");

                    b.ToTable("Minutes");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.AttendingUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAttendance", null)
                        .WithMany("Attendees")
                        .HasForeignKey("MeetingAttendanceId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Attendees")
                        .HasForeignKey("MeetingId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Guest", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Guests")
                        .HasForeignKey("MeetingId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", null)
                        .WithMany("SubItems")
                        .HasForeignKey("MeetingAgendaItemId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Items")
                        .HasForeignKey("MeetingId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAttendance", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithOne("Attendance")
                        .HasForeignKey("GovernancePortal.Core.Meetings.MeetingAttendance", "MeetingId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItem", b =>
                {
                    b.HasOne("GovernancePortal.Core.General.Attachment", "Attachment")
                        .WithMany()
                        .HasForeignKey("AttachmentId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingPack", null)
                        .WithMany("MeetingPackItems")
                        .HasForeignKey("MeetingPackId");

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItemUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.AttendingUser", "AttendingUser")
                        .WithMany()
                        .HasForeignKey("AttendingUserId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingPackItem", null)
                        .WithMany("CoCreators")
                        .HasForeignKey("CoCreatorId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingPackItem", null)
                        .WithMany("InterestTagUsers")
                        .HasForeignKey("InterestTagUserId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingPackItem", null)
                        .WithMany("RestrictedUsers")
                        .HasForeignKey("RestrictedUserId");

                    b.Navigation("AttendingUser");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Minutes", b =>
                {
                    b.HasOne("GovernancePortal.Core.General.Attachment", "Attachment")
                        .WithMany()
                        .HasForeignKey("AttachmentId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithOne("Minutes")
                        .HasForeignKey("GovernancePortal.Core.Meetings.Minutes", "MeetingId");

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Meeting", b =>
                {
                    b.Navigation("Attendance");

                    b.Navigation("Attendees");

                    b.Navigation("Guests");

                    b.Navigation("Items");

                    b.Navigation("Minutes");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.Navigation("SubItems");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAttendance", b =>
                {
                    b.Navigation("Attendees");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPack", b =>
                {
                    b.Navigation("MeetingPackItems");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItem", b =>
                {
                    b.Navigation("CoCreators");

                    b.Navigation("InterestTagUsers");

                    b.Navigation("RestrictedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
