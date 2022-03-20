using System;
using MediatR;
using MegaFortnite.Common.Result;
using MegaFortnite.Engine;

namespace MegaFortnite.Business.Join
{
    public class JointCommand : IRequest<Result<PlayerStats>>
    {
        public Guid CustomerId { get; set; }
        public string LobbyKey { get; set; }
    }
}
