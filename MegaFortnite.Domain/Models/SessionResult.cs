using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaFortnite.Domain.Models
{
    public class SessionResult : BaseEntity
    {
        public Guid SessionId { get; init; }
        public Session Session { get; init; }
        public int GameProfileId { get; init; }
        public Profile GameProfile { get; init; }
        public int Score { get; init; }
    }
}
