using System;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Business.CreateLobby;
using MegaFortnite.Business.GetProfile;
using MegaFortnite.Business.Join;
using MegaFortnite.Common.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Hubs
{
    public class LobbyHub : Hub
    {
        private IMediator _mediator { get; }
        private readonly ILogger<LobbyHub> _logger;

        public LobbyHub(IMediator mediator, ILogger<LobbyHub> logger)
        {
            _mediator = _mediator;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var paramId = Context.GetHttpContext().Request.Query["id"];

            if (!int.TryParse(paramId, out var id))
            {
                _logger.LogWarning("Invalid user id on connection {Id}", paramId);
                await Clients.Others.SendAsync("FailedToConnect", "Invalid user id", Context.ConnectionAborted);
                return;
            }

            var profileResponse = await _mediator.Send(new GetProfileCommand
            {
                Id = id,
            }, Context.ConnectionAborted);

            if (!profileResponse.Success)
            {
                await Clients.Others.SendAsync("LogWarning", profileResponse.Message, Context.ConnectionAborted);
                Context.Abort();
            }

            await Clients.Others.SendAsync("LogMessage", $"{profileResponse.Data.NickName} connected", Context.ConnectionAborted);
        }

        public async Task CreateLobby(int ownerId, SessionType lobbyType = SessionType.Duel)
        {
            var paramId = Context.GetHttpContext().Request.Query["id"];

            if (!int.TryParse(paramId, out var id))
            {
                _logger.LogWarning("Invalid user id on connection {Id}", paramId);
                await Clients.Client(Context.ConnectionId).SendAsync("LogError", "Invalid user id", Context.ConnectionAborted);
                return;
            }

            var lobbyResponse = await _mediator.Send(new CreateLobbyCommand
            {
                OwnerId = id,
                SessionType = lobbyType,
            }, Context.ConnectionAborted);

            if (!lobbyResponse.Success)
            {
                await Clients.Client(Context.ConnectionId)
                    .SendAsync("LogError", new { lobbyResponse.Code, lobbyResponse.Message }, Context.ConnectionAborted);
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyResponse.Data.Key);
            await Clients.Client(Context.ConnectionId)
                .SendAsync("LobbyCreated", new { lobbyResponse.Data.Key, lobbyResponse.Data.Type }, Context.ConnectionAborted);
        }

        public async Task Join(string key)
        {
            var paramId = Context.GetHttpContext().Request.Query["id"];

            if (!int.TryParse(paramId, out var id))
            {
                _logger.LogWarning("Invalid user id on connection {Id}", paramId);
                await Clients.Others.SendAsync("LogWarning", "Invalid user id", Context.ConnectionAborted);
                return;
            }

            var joinResponse = await _mediator.Send(new JointCommand
            {
                LobbyKey = key,
                PlayerId = id,
            }, Context.ConnectionAborted);

            if (joinResponse.Success)
                await Clients.Group(key).SendAsync("LogMessage", $"{joinResponse.Data.NickName}", Context.ConnectionAborted);
            else
                await Clients.Group(key).SendAsync("LogWarning", $"{joinResponse.Message} -+Code: {joinResponse.Code}", Context.ConnectionAborted);
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (exception is not null)
                _logger.LogWarning(exception, "Disconnected with error {ConnectionId} ", Context.ConnectionId);
            else
                _logger.LogInformation("Client {ConnectionId} disconnected.", Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
