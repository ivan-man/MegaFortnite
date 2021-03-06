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
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(CreateLobbyCommand.CustomerId)}");

            RuleFor(x => x.ConnectionId)
                .NotEmpty()
                .WithMessage($"Invalid {nameof(CreateLobbyCommand.ConnectionId)}");
        }
    }
}
