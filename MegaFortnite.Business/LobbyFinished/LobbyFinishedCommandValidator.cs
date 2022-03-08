using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MegaFortnite.Business.LobbyFinished
{
    class LobbyFinishedCommandValidator : AbstractValidator<LobbyFinishedCommand>
    {
        public LobbyFinishedCommandValidator()
        {
            RuleFor(x => x.Winner)
                .GreaterThan(0)
                .WithMessage($"Invalid {nameof(LobbyFinishedCommand.Winner)} Id");

            RuleFor(x => x.Loser)
                .GreaterThan(0)
                .WithMessage($"Invalid {nameof(LobbyFinishedCommand.Loser)} Id");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage($"Invalid session {nameof(LobbyFinishedCommand.Id)}");
        }
    }
}
