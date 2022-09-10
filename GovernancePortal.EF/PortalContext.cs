using GovernancePortal.Core.TaskManagement;
using GovernancePortal.EF.ModelConfig.TaskManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.EF
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions<PortalContext> context) : base(context)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<TaskModel>(new TaskConfig());
            base.OnModelCreating(builder);
        }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskPerson> TaskPersons { get; set; }
        public DbSet<MeetingModel> MeetingModels { get; set; }
        public DbSet<MeetingPack> MeetingPacks { get; set; }
        public DbSet<MeetingAgendaItem> MeetingAgendaItems { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
    }
}
