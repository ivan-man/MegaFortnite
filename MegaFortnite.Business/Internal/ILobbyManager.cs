using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Result;
using MegaFortnite.Engine;

namespace MegaFortnite.Business.Internal
{
    public interface ILobbyManager
    {
        Result<Lobby> GetLobby(string key);
        Result<Lobby> CreateLobby(int ownerId, SessionType sessionType);
    }
}
