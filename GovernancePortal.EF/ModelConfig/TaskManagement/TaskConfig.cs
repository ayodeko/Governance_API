using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.EF.ModelConfig.TaskManagement
{
    public class TaskConfig : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(fk => fk.TaskId);
            builder.HasMany(x => x.Participants)
              .WithOne()
              .HasForeignKey(fk => fk.TaskId);
        }
    }

    public static class TaskConfigSettings
    {
        public static ModelBuilder AddTaskConfigs(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TaskConfig());
            return builder;
        }
    }

}
