using GovernancePortal.Core.Resolutions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GovernancePortal.EF.ModelConfig.Resolutions;

public class ResolutionConfig
{
    
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