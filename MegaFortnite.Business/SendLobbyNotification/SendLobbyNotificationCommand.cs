using MediatR;
using MegaFortnite.Common.Result;

namespace MegaFortnite.Business.SendLobbyNotification
{
    public class SendLobbyNotificationCommand : IRequest<Result>
    {
        public string LobbyKey { get; set; }
        public string Message { get; set; }
    }
}
