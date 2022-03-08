using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MegaFortnite.Business.CreateLobby
{
    public class CreateLobbyCommandValidator : AbstractValidator<CreateLobbyCommand>
    {
        public CreateLobbyCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .GreaterThan(0)
                .WithMessage($"Invalid {nameof(CreateLobbyCommand.OwnerId)}");

            RuleFor(x => x.ConnectionId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(CreateLobbyCommand.ConnectionId)}");
        }
    }
}
