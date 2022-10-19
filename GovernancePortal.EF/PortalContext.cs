using GovernancePortal.Core.TaskManagement;
using GovernancePortal.EF.ModelConfig.TaskManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.Meetings;
using GovernancePortal.EF.ModelConfig.Meetings;

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
            base.OnModelCreating(builder);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingPack> MeetingPacks { get; set; }
        public DbSet<MeetingAgendaItem> MeetingAgendaItems { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
        public DbSet<AttendingUser> AttendingUsers { get; set; }
        public DbSet<Minutes> Minutes { get; set; }
        public DbSet<MeetingPackItemUser> MeetingPackItemUsers { get; set; }
    }
}
