using System;
using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Result;
using MegaFortnite.Engine;

namespace MegaFortnite.Business.InternalServices
{
    public interface ILobbyManager
    {
        Result<Lobby> GetLobby(string key);
        Result<Lobby> CreateLobby(Guid customerId, SessionType sessionType);
        Result SetOrUpdateConnectionId(Guid customerId, string sessionId);
        Result<string> GetConnectionId(Guid customerId);
    }
}
