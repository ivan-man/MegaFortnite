using System;
using System.Collections.Generic;

namespace MegaFortnite.Engine
{
    public class StatesChangeEventArgs
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public int? Winner { get; set; }
        public int? Loser { get; set; }
        public IEnumerable<PlayerStats> Stats { get; set; }
    }
}
