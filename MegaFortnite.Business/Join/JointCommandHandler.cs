using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using MegaFortnite.Business.GetProfile;
using MegaFortnite.Business.Internal;
using MegaFortnite.Common.Result;
using MegaFortnite.Contracts.Dto;
using MegaFortnite.Engine;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.Join
{
    public class JointCommandHandler : IRequestHandler<JointCommand, Result<PlayerStats>>
    {
        private readonly IMediator _mediator;
        private readonly ILobbyManager _lobbyManager;
        private readonly ILogger<JointCommandHandler> _logger;

        public JointCommandHandler(IMediator mediator, ILobbyManager lobbyManager, ILogger<JointCommandHandler> logger)
        {
            _mediator = mediator;
            _lobbyManager = lobbyManager;
            _logger = logger;
        }

        public async Task<Result<PlayerStats>> Handle(JointCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var profileResponse = await _mediator.Send(new GetProfileCommand { CustomerId = request.CustomerId });

                if (!profileResponse.Success)
                    return Result<PlayerStats>.Failed(profileResponse);

                var lobbyResponse = _lobbyManager.GetLobby(request.LobbyKey);

                return !lobbyResponse.Success
                    ? Result<PlayerStats>.Failed(lobbyResponse)
                    : lobbyResponse.Data.Join(profileResponse.Data.Adapt<PlayerProfileDto>());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to join to lobby {@Request}", request);
                return Result<PlayerStats>.Internal(e.Message);
            }
        }
    }
}
