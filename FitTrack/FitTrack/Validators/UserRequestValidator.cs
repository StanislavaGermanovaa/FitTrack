using FitTrack.Models.Request;
using FluentValidation;

namespace FitTrack.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .Length(2, 100).WithMessage("First Name must be between 2 and 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .Length(2, 100).WithMessage("Last Name must be between 2 and 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.");
        }
    }
}
