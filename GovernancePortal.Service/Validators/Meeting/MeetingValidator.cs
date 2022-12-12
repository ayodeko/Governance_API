using FluentValidation;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Service.Validators.Meeting;

public class MeetingValidator : AbstractValidator<Core.Meetings.Meeting>
{
    public MeetingValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("The meeting title cannot be null, please pass in a title to continue")
            .NotEmpty()
            .WithMessage("The meeting title cannot be empty, please pass in a title to continue");
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("The passed value is not contained in the enum for Meeting Type");
        RuleFor(x => x.Frequency)
            .IsInEnum()
            .WithMessage("The passed value is not contained in the enum for Meeting Frequency");
        RuleFor(x => x.Medium)
            .IsInEnum()
            .WithMessage("The passed value is not contained in the enum for Meeting Medium");
    }
}

public class AttendingUserValidator : AbstractValidator<AttendingUser>
{
    public AttendingUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is a required field for adding Attending Users");
    }
}