using System;
using System.Linq;

namespace MegaFortnite.Common.Helpers
{
    public static class LobbyKeyGenerator
    {
        private static readonly Random Random = new ();

        public static string GenerateKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
