using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Business.InternalServices;
using MegaFortnite.Common.Result;
using MegaFortnite.DataAccess;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.SetOrUpdateConnection
{
    public class SetOrUpdateConnectionCommandHandler : IRequestHandler<SetOrUpdateConnectionCommand, Result>
    {
        private readonly ILobbyManager _lobbyManager;
        private readonly ILogger<SetOrUpdateConnectionCommandHandler> _logger;

        public SetOrUpdateConnectionCommandHandler(
            ILobbyManager lobbyManager,
            ILogger<SetOrUpdateConnectionCommandHandler> logger)
        {
            _lobbyManager = lobbyManager;
            _logger = logger;
        }

        public async Task<Result> Handle(SetOrUpdateConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return _lobbyManager.SetOrUpdateConnectionId(request.CustomerId, request.ConnectionId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to set ConnectionId. {@Request}", request);
                return Result.Internal(e.Message);
            }
        }
    }
}
