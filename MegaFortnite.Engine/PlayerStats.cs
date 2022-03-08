using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaFortnite.Engine
{
    public class PlayerStats
    {
        public int PlayerId { get; }
        public string NickName { get; }
        public int MaxHealth { get; }


        private int _prevHealth;
        public int PrevHealth { get => _prevHealth; }


        private int _health;
        public int Health { get => _health; }

        public PlayerStats(int playerId, string nickName, int maxHealth = 10)
        {
            PlayerId = playerId;
            NickName = nickName;
            MaxHealth = maxHealth;
            _health = maxHealth;
        }

        public void Heal(int value)
        {
            if (value <= 0)
                return;

            if (MaxHealth == int.MaxValue)
                return;

            _prevHealth = _health;
            _health = _health + value > MaxHealth ? MaxHealth : _health + value;
        }

        public void Damage(int value)
        {
            if (value <= 0)
                return;

            if(MaxHealth == int.MaxValue)
                return;

            _prevHealth = _health;
            _health = _health - value < 0 ? 0 : _health - value;
        }
    }
}
