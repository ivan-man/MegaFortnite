using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Result;
using MegaFortnite.DataAccess;
using MegaFortnite.Domain.Models;
using MegaFortnite.Engine;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.LobbyFinished
{
    public class LobbyFinishedCommandHandler : IRequestHandler<LobbyFinishedCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LobbyFinishedCommandHandler> _logger;

        public LobbyFinishedCommandHandler(IUnitOfWork unitOfWork, ILogger<LobbyFinishedCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> Handle(LobbyFinishedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var session =
                    await _unitOfWork.Sessions.GetAsync(q => q.Id == request.Id, cancellationToken: cancellationToken);
                if (session is not null)
                    session.State = SessionState.Finished;

                var winnerResult = new SessionResult
                {
                    GameProfileId = request.Winner,
                    Score = 1,
                    SessionId = request.Id
                };

                var loserResult = new SessionResult
                {
                    GameProfileId = request.Loser,
                    Score = -1,
                    SessionId = request.Id
                };

                _unitOfWork.Results.Add(loserResult);
                _unitOfWork.Results.Add(winnerResult);

                var loser = await _unitOfWork.Profiles.GetAsync(q => q.Id == request.Loser,
                    cancellationToken: cancellationToken);

                if (loser is not null)
                    loser.Rate--;

                var winner = await _unitOfWork.Profiles.GetAsync(q => q.Id == request.Winner,
                    cancellationToken: cancellationToken);

                if (winner is not null)
                    winner.Rate++;

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return Result.Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to finish lobby {@Request}", request);
                return Result.Internal(e.Message);
            }
        }
    }
}
