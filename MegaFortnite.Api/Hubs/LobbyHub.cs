using System;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using MegaFortnite.Business.CreateLobby;
using MegaFortnite.Business.GetProfile;
using MegaFortnite.Business.Join;
using MegaFortnite.Business.SetOrUpdateConnection;
using MegaFortnite.Common.Enums;
using MegaFortnite.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Api.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LobbyHub> _logger;

        public LobbyHub(IMediator mediator, ILogger<LobbyHub> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;

            var userId = Context.User?.Claims
                .FirstOrDefault(q =>
                    q.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))
                ?.Value;

            if (!Guid.TryParse(userId, out var customerId))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("LogWarning", $"Invalid UserId {userId}");
                return;
            }

            var profileResponse = await _mediator.Send(new GetProfileCommand
            {
                CustomerId = customerId,
            }, Context.ConnectionAborted);

            if (!profileResponse.Success)
            {
                await Clients.Others.SendAsync("LogWarning", profileResponse.Message ?? profileResponse.Code.ToString(),
                    Context.ConnectionAborted);
                Context.Abort();
            }

            var setConnectionResponse = await _mediator.Send(new SetOrUpdateConnectionCommand
            {
                CustomerId = customerId,
                ConnectionId = connectionId,
            }, Context.ConnectionAborted);

            if (!setConnectionResponse.Success)
            {
                await Clients.Others.SendAsync("LogWarning",
                    setConnectionResponse.Message ?? setConnectionResponse.Code.ToString(), Context.ConnectionAborted);
                Context.Abort();
            }

            await Clients.Client(Context.ConnectionId)
                .SendAsync("LogMessage", $"{profileResponse.Data.NickName} connected");
        }

        public async Task CreateLobby(string customerIdStr)
        {
            if (!Guid.TryParse(customerIdStr, out var customerId))
            {
                await Clients.Others.SendAsync("LogWarning", "Failed to create lobby. Invalid CustomerId",
                    Context.ConnectionAborted);
            }

            var lobbyType = SessionType.Duel;

            var lobbyResponse = await _mediator.Send(new CreateLobbyCommand
            {
                CustomerId = customerId,
                SessionType = lobbyType,
                ConnectionId = Context.ConnectionId,
            }, Context.ConnectionAborted);

            if (!lobbyResponse.Success)
            {
                await Clients.Client(Context.ConnectionId)
                    .SendAsync("LogError", new { lobbyResponse.Code, lobbyResponse.Message },
                        Context.ConnectionAborted);
                return;
            }

            //lobbyResponse.Data.StartedEvent += DataOnStartedEvent;
            //lobbyResponse.Data.TickHappened += DataOnTickHappened;
            //lobbyResponse.Data.FinishedEvent += DataOnFinishedEvent;

            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyResponse.Data.Key, Context.ConnectionAborted);
            await Clients.Client(Context.ConnectionId)
                .SendAsync("LobbyCreated", new { lobbyResponse.Data.Key, lobbyResponse.Data.Type },
                    Context.ConnectionAborted);
        }

        private void DataOnFinishedEvent(object sender, StatesChangeEventArgs e)
        {
            Clients.Group(e.Key).SendAsync("LogWarning",
                string.Join("; ", e.Stats.Select(q => $"{q.NickName}: {q.Health}")));

            var winner = e.Stats.Where(q => q.Health > 0)?.FirstOrDefault();
            var looser = e.Stats.Where(q => q.Health <= 0)?.FirstOrDefault();

            Clients.Group(e.Key).SendAsync("LogWarning", $"{looser?.NickName} WIN");
            Clients.Group(e.Key).SendAsync("LogWarning", $"{looser?.NickName} LOSE");
        }

        private void DataOnTickHappened(object sender, StatesChangeEventArgs e)
        {
            Clients.Group(e.Key).SendAsync("LogMessage",
                string.Join("; ", e.Stats.Select(q => $"{q.NickName}: {q.Health}")));
        }

        private void DataOnStartedEvent(object sender, StatesChangeEventArgs e)
        {
            Clients.Group(e.Key).SendAsync("LogMessage",
                string.Join("; ", e.Stats.Select(q => $"{q.NickName}: {q.Health}")));
        }

        public async Task Join(Guid customerId, string key)
        {
            var joinResponse = await _mediator.Send(new JointCommand
            {
                LobbyKey = key,
                CustomerId = customerId,
            }, Context.ConnectionAborted);

            if (joinResponse.Success)
            {
                await Clients.Group(key).SendAsync("LogMessage", $"{joinResponse.Data.NickName} joined",
                    Context.ConnectionAborted);
                await Groups.AddToGroupAsync(this.Context.ConnectionId, key);
            }
            else
                await Clients.Group(key).SendAsync("LogWarning", $"{joinResponse.Message} Code: {joinResponse.Code}",
                    Context.ConnectionAborted);
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
