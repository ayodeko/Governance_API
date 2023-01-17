using GovernancePortal.Core.TaskManagement;
using GovernancePortal.EF.ModelConfig.TaskManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.Bridges;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.EF.ModelConfig.Meetings;
using GovernancePortal.EF.ModelConfig.Resolutions;

namespace GovernancePortal.EF
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> context) : base(context)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration<TaskModel>(new TaskConfig());
            builder.AddMeetingConfigs();
            builder.AddResolutionConfigs();
            builder.AddTaskConfigs();
            base.OnModelCreating(builder);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingPack> MeetingPacks { get; set; }
        public DbSet<MeetingAgendaItem> MeetingAgendaItems { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
        public DbSet<AttendingUser> AttendingUsers { get; set; }
        public DbSet<Minute> Minutes { get; set; }
        public DbSet<NoticeMeeting> Notices { get; set; }
        public DbSet<MeetingPackItemUser> MeetingPackItemUsers { get; set; }
        public DbSet<Meeting_Resolution> Meeting_Resolutions { get; set; }
        public DbSet<Task_Resolution> Task_Resolutions { get; set; }
        public DbSet<Meeting_Task> Meeting_Tasks { get; set; }
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Poll> Polls { get; set; }
        
        //task management
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
