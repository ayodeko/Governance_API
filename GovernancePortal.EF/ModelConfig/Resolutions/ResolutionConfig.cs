using GovernancePortal.Core.Bridges;
using GovernancePortal.Core.Resolutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovernancePortal.EF.ModelConfig.Resolutions;

public class ResolutionConfig
{
    
}
public static class ResolutionConfigSettings
{
    public static ModelBuilder AddResolutionConfigs(this ModelBuilder builder)
    {
        
        builder.ApplyConfiguration(new Meeting_ResolutionConfig());
        return builder;
    }
}
public class VotingConfig : IEntityTypeConfiguration<Voting>
{
    public void Configure(EntityTypeBuilder<Voting> builder)
    {
        
    }
}

public class PollUserConfig : IEntityTypeConfiguration<PollUser>
{
    public void Configure(EntityTypeBuilder<PollUser> builder)
    {
      
    }
}


public class Meeting_ResolutionConfig : IEntityTypeConfiguration<Meeting_Resolution>
{
    public void Configure(EntityTypeBuilder<Meeting_Resolution> builder)
    {
        builder.HasKey(ky => new { ky.MeetingId, ky.ResolutionId });
        builder.HasIndex(ky => ky.ResolutionId).IsUnique();
        builder.HasIndex(ky => ky.MeetingId).IsUnique();
    }
}