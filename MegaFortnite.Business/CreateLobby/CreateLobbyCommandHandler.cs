using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using MegaFortnite.Business.Internal;
using MegaFortnite.Common.Result;
using MegaFortnite.DataAccess;
using MegaFortnite.Domain.Models;
using MegaFortnite.Engine;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.CreateLobby
{
    public class CreateLobbyCommandHandler : IRequestHandler<CreateLobbyCommand, Result<Lobby>>
    {
        private ILobbyManager _lobbyManager;
        private IUnitOfWork _unitOfWork;
        private ILogger<CreateLobbyCommandHandler> _logger;

        public CreateLobbyCommandHandler(ILobbyManager lobbyManager, IUnitOfWork unitOfWork, ILogger<CreateLobbyCommandHandler> logger)
        {
            _lobbyManager = lobbyManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Lobby>> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lobbyResponse = _lobbyManager.CreateLobby(request.OwnerId, request.SessionType);
                if (!lobbyResponse.Success)
                {
                    _logger.LogWarning("Failed to create lobby. {@Request}", request);
                    return Result<Lobby>.Failed(lobbyResponse);
                }

                var session = lobbyResponse.Data.Adapt<Session>();
                _unitOfWork.Sessions.Add(session);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                lobbyResponse.Data.Id = session.Id;

                return lobbyResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create lobby. {@Request}", request);
                return Result<Lobby>.Internal();
            }
        }
    }
}
