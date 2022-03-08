using MediatR;
using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Result;
using MegaFortnite.Engine;

namespace MegaFortnite.Business.CreateLobby
{
    public class CreateLobbyCommand : IRequest<Result<Lobby>>
    {
        public int OwnerId { get; set; }
        public string ConnectionId { get; set; }
        public SessionType SessionType { get; set; }
    }
}
