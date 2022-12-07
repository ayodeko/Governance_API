﻿// <auto-generated />
using System;
using GovernancePortal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GovernancePortal.EF.Migrations
{
    [DbContext(typeof(PortalContext))]
    partial class PortalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, (int)1L, 1);

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

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

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
                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AttendeePosition")
                        .HasColumnType("int");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGuest")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMinuteApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingAttendanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MeetingId", "UserId");

                    b.HasIndex("MeetingAttendanceId");

                    b.ToTable("AttendingUsers");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttendanceGeneratedCode")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<bool>("IsAttendanceTaken")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMeetingPackDownloadable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMinutesPublished")
                        .HasColumnType("bit");

                    b.Property<int>("Medium")
                        .HasColumnType("int");

                    b.Property<string>("MeetingPackId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("SecretaryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ActionRequired")
                        .HasColumnType("int");

                    b.Property<string>("AgendaItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttachmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNumbered")
                        .HasColumnType("bit");

                    b.Property<string>("MeetIdHolder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PresenterUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("MeetingId");

                    b.HasIndex("ParentId");

                    b.ToTable("MeetingAgendaItems");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAttendance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeneratedCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

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

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingAgendaItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingPackId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("PresenterUserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("MeetingId");

                    b.HasIndex("MeetingPackId");

                    b.ToTable("MeetingPackItem");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItemUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgendaItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendingUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendingUserMeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttendingUserUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CoCreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterestTagUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MeetingIdHolder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestrictedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CoCreatorId");

                    b.HasIndex("InterestTagUserId");

                    b.HasIndex("RestrictedUserId");

                    b.HasIndex("AttendingUserMeetingId", "AttendingUserUserId");

                    b.ToTable("MeetingPackItemUsers");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Minute", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgendaItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttachmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MinuteText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgendaItemId");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("MeetingId");

                    b.ToTable("Minutes");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.NoticeMeeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgendaText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Mandate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeetingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("NoticeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoticeText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salutation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendNoticeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Signatory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignatureId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("MeetingId")
                        .IsUnique()
                        .HasFilter("[MeetingId] IS NOT NULL");

                    b.HasIndex("SignatureId");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.Poll", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MaximumSelection")
                        .HasColumnType("int");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isUnlimitedSelection")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PollId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollItem");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollItemVote", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PollItemId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PollUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PollUserId");

                    b.ToTable("PollItemVote");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PollId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollUser");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.Voting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVotingEnded")
                        .HasColumnType("bit");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Votings");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.VotingUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ModelStatus")
                        .HasColumnType("int");

                    b.Property<int>("Stance")
                        .HasColumnType("int");

                    b.Property<string>("StanceReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VotingId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VotingId");

                    b.ToTable("VotingUser");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.AttendingUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAttendance", null)
                        .WithMany("Attendees")
                        .HasForeignKey("MeetingAttendanceId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Attendees")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.HasOne("GovernancePortal.Core.General.Attachment", "Attachment")
                        .WithMany()
                        .HasForeignKey("AttachmentId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Items")
                        .HasForeignKey("MeetingId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", null)
                        .WithMany("SubItems")
                        .HasForeignKey("ParentId");

                    b.Navigation("Attachment");
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

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Packs")
                        .HasForeignKey("MeetingId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingPack", null)
                        .WithMany("MeetingPackItems")
                        .HasForeignKey("MeetingPackId");

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingPackItemUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", null)
                        .WithMany("CoCreators")
                        .HasForeignKey("CoCreatorId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", null)
                        .WithMany("InterestTagUsers")
                        .HasForeignKey("InterestTagUserId");

                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", null)
                        .WithMany("RestrictedUsers")
                        .HasForeignKey("RestrictedUserId");

                    b.HasOne("GovernancePortal.Core.Meetings.AttendingUser", "AttendingUser")
                        .WithMany()
                        .HasForeignKey("AttendingUserMeetingId", "AttendingUserUserId");

                    b.Navigation("AttendingUser");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Minute", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.MeetingAgendaItem", "AgendaItem")
                        .WithMany()
                        .HasForeignKey("AgendaItemId");

                    b.HasOne("GovernancePortal.Core.General.Attachment", "Attachment")
                        .WithMany()
                        .HasForeignKey("AttachmentId");

                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithMany("Minutes")
                        .HasForeignKey("MeetingId");

                    b.Navigation("AgendaItem");

                    b.Navigation("Attachment");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.NoticeMeeting", b =>
                {
                    b.HasOne("GovernancePortal.Core.Meetings.Meeting", null)
                        .WithOne("Notice")
                        .HasForeignKey("GovernancePortal.Core.Meetings.NoticeMeeting", "MeetingId");

                    b.HasOne("GovernancePortal.Core.General.Attachment", "Signature")
                        .WithMany()
                        .HasForeignKey("SignatureId");

                    b.Navigation("Signature");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollItem", b =>
                {
                    b.HasOne("GovernancePortal.Core.Resolutions.Poll", null)
                        .WithMany("PollItems")
                        .HasForeignKey("PollId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollItemVote", b =>
                {
                    b.HasOne("GovernancePortal.Core.Resolutions.PollUser", null)
                        .WithMany("PollVotes")
                        .HasForeignKey("PollUserId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Resolutions.Poll", null)
                        .WithMany("PollUsers")
                        .HasForeignKey("PollId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.VotingUser", b =>
                {
                    b.HasOne("GovernancePortal.Core.Resolutions.Voting", null)
                        .WithMany("Voters")
                        .HasForeignKey("VotingId");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.Meeting", b =>
                {
                    b.Navigation("Attendance");

                    b.Navigation("Attendees");

                    b.Navigation("Items");

                    b.Navigation("Minutes");

                    b.Navigation("Notice");

                    b.Navigation("Packs");
                });

            modelBuilder.Entity("GovernancePortal.Core.Meetings.MeetingAgendaItem", b =>
                {
                    b.Navigation("CoCreators");

                    b.Navigation("InterestTagUsers");

                    b.Navigation("RestrictedUsers");

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

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.Poll", b =>
                {
                    b.Navigation("PollItems");

                    b.Navigation("PollUsers");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.PollUser", b =>
                {
                    b.Navigation("PollVotes");
                });

            modelBuilder.Entity("GovernancePortal.Core.Resolutions.Voting", b =>
                {
                    b.Navigation("Voters");
                });
#pragma warning restore 612, 618
        }
    }
}
