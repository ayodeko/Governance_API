using FluentValidation;
using GovernancePortal.Core.Resolutions;

namespace GovernancePortal.Service.Validators.Resolution;

public class VotingValidator : AbstractValidator<Voting>
{
    public VotingValidator()
    {
        
    }
}
public class VotingUserValidator : AbstractValidator<VotingUser>
{
    public VotingUserValidator()
    {
        RuleFor(x => x.Stance)
            .IsInEnum()
            .WithMessage("The passed value is not contained in the enum for Voting Stance");
    }
}