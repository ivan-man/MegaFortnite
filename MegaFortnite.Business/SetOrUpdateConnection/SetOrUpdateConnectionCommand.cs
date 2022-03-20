using System;
using MediatR;
using MegaFortnite.Common.Result;

namespace MegaFortnite.Business.SetOrUpdateConnection
{
    public class SetOrUpdateConnectionCommand : IRequest<Result>
    {
        public Guid CustomerId { get; set; }
        public string ConnectionId { get; set; }
    }
}
