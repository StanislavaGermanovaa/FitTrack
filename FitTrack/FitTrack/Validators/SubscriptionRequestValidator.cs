using FitTrack.Models.Request;
using FluentValidation;

namespace FitTrack.Validators
{
    public class SubscriptionRequestValidator : AbstractValidator<SubscriptionRequest>
    {
        public SubscriptionRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Subscription Type is required.")
                .Length(3, 50).WithMessage("Subscription Type must be between 3 and 50 characters.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start Date must be before End Date.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("End Date must be after Start Date.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Active status must be specified.");
        }
    }
}
