using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using MegaFortnite.Business.InternalServices;
using MegaFortnite.Common.Result;
using MegaFortnite.DataAccess;
using MegaFortnite.Domain.Models;
using MegaFortnite.Engine;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.CreateLobby
{
    public class CreateLobbyCommandHandler : IRequestHandler<CreateLobbyCommand, Result<Lobby>>
    {
        private readonly ILobbyManager _lobbyManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateLobbyCommandHandler> _logger;

        public CreateLobbyCommandHandler(ILobbyManager lobbyManager, IUnitOfWork unitOfWork,
            ILogger<CreateLobbyCommandHandler> logger)
        {
            _lobbyManager = lobbyManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Lobby>> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lobbyResponse = _lobbyManager.CreateLobby(request.CustomerId, request.SessionType);
                if (!lobbyResponse.Success)
                {
                    _logger.LogWarning("Failed to create lobby. {@Request}", request);
                    return Result<Lobby>.Failed(lobbyResponse);
                }

                var session = lobbyResponse.Data.Adapt<Session>();
                if (await _unitOfWork.Sessions.CountAsync(q => q.LobbyKey == session.LobbyKey, cancellationToken) < 1)
                {
                    _unitOfWork.Sessions.Add(session);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
                else
                    return lobbyResponse;

                lobbyResponse.Data.Id = session.Id;

                return lobbyResponse;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create lobby. {@Request}", request);
                return Result<Lobby>.Internal(e.Message);
            }
        }
    }
}
