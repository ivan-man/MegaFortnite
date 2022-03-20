using System;

namespace MegaFortnite.Contracts.Dto
{
    public class PlayerProfileDto
    {
        public int Id { get; init; }
        public string NickName { get; init; }
        public Guid CustomerId { get; init; }
        public CustomerDto Customer { get; init; }
        public int WinRate { get; init; }
        public int Rate { get; init; }


        public override bool Equals(object? obj)
        {
            var other = obj as PlayerProfileDto;
            if (other is null)
                return false;

            return NickName.Equals(other.NickName) && CustomerId.Equals(other.CustomerId);
        }

        public override int GetHashCode()
        {
            return 37 ^ NickName.GetHashCode() ^ CustomerId.GetHashCode();
        }
    }
}
