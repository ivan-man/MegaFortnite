using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MegaFortnite.Common.Enums;

namespace MegaFortnite.Domain.Models
{
    public class Session : BaseEntity<Guid>
    {
        [MaxLength(5)] public string LobbyKey { get; init; }
        public SessionType Type { get; init; }
        public SessionState State { get; set; }
        public Profile Owner { get; init; }
        public int OwnerId { get; init; }
        public List<SessionResult> Results { get; init; }
    }
}
