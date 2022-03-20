using FluentValidation;

namespace MegaFortnite.Business.SendLobbyNotification
{
    public class SendLobbyNotificationCommandValidator : AbstractValidator<SendLobbyNotificationCommand>
    {
        public SendLobbyNotificationCommandValidator()
        {
            RuleFor(x => x.LobbyKey)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(SendLobbyNotificationCommand.LobbyKey)}");

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage($"Empty {nameof(SendLobbyNotificationCommand.Message)}");
        }
    }
}
