using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Common.Interfaces;
using MegaFortnite.Common.Result;
using MegaFortnite.DataAccess;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.SendLobbyNotification
{
    public class SendLobbyNotificationCommandHandler : IRequestHandler<SendLobbyNotificationCommand, Result>
    {
        private readonly INotifyService _notifyService;
        private readonly ILogger<SendLobbyNotificationCommandHandler> _logger;

        public SendLobbyNotificationCommandHandler(INotifyService notifyService,
            ILogger<SendLobbyNotificationCommandHandler> logger)
        {
            _notifyService = notifyService;
            _logger = logger;
        }

        public async Task<Result> Handle(SendLobbyNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _notifyService.SendNotificationAsync(request.LobbyKey, request.Message);
                return Result.Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to send notification {@Request}", request);
                return Result.Internal(e.Message);
            }
        }
    }
}
