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
        public IMediator _mediator;
        public ILobbyManager _lobbyManager;
        private ILogger<JointCommandHandler> _logger;

        public JointCommandHandler(IMediator mediator, ILobbyManager lobbyManager, ILogger<JointCommandHandler> logger)
        {
            _mediator = mediator;
            _lobbyManager = lobbyManager;
            _logger = logger;
        }

        public async Task<Result<PlayerStats>> Handle(JointCommand request, CancellationToken cancellationToken)
        {
            var profileResponse = await _mediator.Send(new GetProfileCommand { Id = request.PlayerId });

            if (!profileResponse.Success)
                return Result<PlayerStats>.Failed(profileResponse);

            var lobbyResponse = _lobbyManager.GetLobby(request.LobbyKey);

            if (!lobbyResponse.Success)
                return Result<PlayerStats>.Failed(lobbyResponse);

            return  lobbyResponse.Data.Join(profileResponse.Data.Adapt<PlayerProfileDto>());
        }
    }
}
