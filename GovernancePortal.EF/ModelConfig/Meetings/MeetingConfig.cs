using GovernancePortal.Core.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovernancePortal.EF.ModelConfig.Meetings;

public class MeetingConfig : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        throw new System.NotImplementedException();
    }

    
}

public static class MeetingConfigSettings
{
    public static ModelBuilder AddMeetingConfigs(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MeetingPackItemConfig());
        builder.ApplyConfiguration(new MeetingAgendaItemConfig());
        builder.ApplyConfiguration(new MeetingAttendanceConfig());
        builder.ApplyConfiguration(new MeetingAttendingUserConfig());
        builder.ApplyConfiguration(new MeetingPackItemUserConfig());
        return builder;
    }
}

public class MeetingPackItemConfig : IEntityTypeConfiguration<MeetingPackItem>
{
    public void Configure(EntityTypeBuilder<MeetingPackItem> builder)
    {
        builder.HasMany(x => x.CoCreators)
            .WithOne()
            .HasForeignKey(fk => fk.CoCreatorId);
        builder.HasMany(x => x.RestrictedUsers)
            .WithOne()
            .HasForeignKey(fk => fk.RestrictedUserId);
        
        builder.HasMany(x => x.InterestTagUsers)
            .WithOne()
            .HasForeignKey(fk => fk.InterestTagUserId);
    }
}

public class MeetingAgendaItemConfig : IEntityTypeConfiguration<MeetingAgendaItem>
{
    public void Configure(EntityTypeBuilder<MeetingAgendaItem> builder)
    {
        builder.HasMany(x => x.SubItems)
            .WithOne()
            .HasForeignKey(fk => fk.ParentId);
    }
}

public class MeetingAttendanceConfig : IEntityTypeConfiguration<MeetingAttendance>
{
    public void Configure(EntityTypeBuilder<MeetingAttendance> builder)
    {
        builder.HasMany(x => x.Attendees)
            .WithOne()
            .HasForeignKey(fk => fk.MeetingAttendanceId);
    }
}

public class MeetingAttendingUserConfig : IEntityTypeConfiguration<AttendingUser>
{ 
    public void Configure(EntityTypeBuilder<AttendingUser> builder)
    {
        builder.HasKey(x => new { x.MeetingId, x.UserId });
    }
}

public class MeetingPackItemUserConfig : IEntityTypeConfiguration<MeetingPackItemUser>
{ 
    public void Configure(EntityTypeBuilder<MeetingPackItemUser> builder)
    {
        
    }
}





