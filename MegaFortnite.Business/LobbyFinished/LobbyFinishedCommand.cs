using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MegaFortnite.Common.Result;

namespace MegaFortnite.Business.LobbyFinished
{
    public class LobbyFinishedCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public int Winner { get; set; }
        public int Loser { get; set; }
    }
}
