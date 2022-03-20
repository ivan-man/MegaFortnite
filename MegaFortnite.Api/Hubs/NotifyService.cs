using System.Threading;
using System.Threading.Tasks;
using MegaFortnite.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace MegaFortnite.Api.Hubs
{
    public class NotifyService : INotifyService
    {
        private readonly IHubContext<LobbyHub> _hub;

        public NotifyService(IHubContext<LobbyHub> hub)
        {
            _hub = hub;
        }

        public Task SendNotificationAsync(string lobbyKey, string message)
        {
            return _hub.Clients.Group(lobbyKey).SendAsync("LogMessage", $"{message}", CancellationToken.None);
        }
    }
}
