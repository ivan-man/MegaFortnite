using FluentValidation;

namespace MegaFortnite.Business.SetOrUpdateConnection
{
    public class SetOrUpdateConnectionCommandValidator: AbstractValidator<SetOrUpdateConnectionCommand>
    {
        public SetOrUpdateConnectionCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(SetOrUpdateConnectionCommand.CustomerId)}");

            RuleFor(x => x.ConnectionId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(SetOrUpdateConnectionCommand.ConnectionId)}");
        }

    }
}
