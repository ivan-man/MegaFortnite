using System;
using System.Collections.Generic;
using MegaFortnite.Common.Result;
using MegaFortnite.Contracts.Dto;

namespace MegaFortnite.Engine
{
    public interface ILobby : IDisposable
    {
        Result Begin();
        Result<PlayerStats> Join(PlayerProfileDto profile);
        Result Leave(PlayerProfileDto profile);

        event Lobby.SampleEventHandler StartedEvent;
        event Lobby.SampleEventHandler FinishedEvent;
        event Lobby.SampleEventHandler TickHappened;
    }
}
