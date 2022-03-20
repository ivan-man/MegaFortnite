using FluentValidation;

namespace MegaFortnite.Business.Join
{
    public class JointCommandValidator : AbstractValidator<JointCommand>
    {
        public JointCommandValidator()
        {
            RuleFor(x => x.LobbyKey)
                .NotEmpty()
                .WithMessage($" {nameof(JointCommand.LobbyKey)} can't be empty");

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(JointCommand.CustomerId)}");
        }
    }
}
