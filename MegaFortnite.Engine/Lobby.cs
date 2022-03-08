using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Result;
using MegaFortnite.Contracts.Dto;

namespace MegaFortnite.Engine
{
    public class Lobby : ILobby
    {
        private readonly Timer _idleTimer;
        private Timer _tickRateTimer;

        private const int TickRate = 1000;
        private const int DamageRange = 2;

        private readonly Random _generator = new Random(37);

        public delegate void SampleEventHandler(object sender, StatesChangeEventArgs e);

        public event SampleEventHandler StartedEvent;
        public event SampleEventHandler FinishedEvent;
        public event SampleEventHandler TickHappened;

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Key { get; init; }
        public int OwnerId { get; init; }
        public SessionType Type { get; init; }
        public SessionState State { get; set; } = SessionState.Pending;
        public DateTime Created { get; init; }
        public ConcurrentDictionary<PlayerProfileDto, PlayerStats> PlayersStats { get; } = new();

        private readonly Func<int, Result> _selfDestruct;

        public Lobby(int ownerId, SessionType type, string key, Func<int, Result> selfDestruct)
        {
            _idleTimer = new Timer(GetSelfDestructPeriod(Type));
            OwnerId = ownerId;
            Key = key;
            Type = type;

            _selfDestruct = selfDestruct;

            Created = DateTime.UtcNow;

            _idleTimer.Start();
            _idleTimer.Elapsed += IdleTimerOnElapsed;
        }


        public void RaiseStartedEvent()
        {
            StartedEvent?.Invoke(this, new StatesChangeEventArgs
            {
                Id = Id,
                Key = Key,
                Stats = PlayersStats.Values,
            });

        }

        public void RaiseTickEvent()
        {
            TickHappened?.Invoke(this, new StatesChangeEventArgs
            {
                Id = Id,
                Key = Key,
                Stats = PlayersStats.Values,
            });
        }

        public void RaiseFinishedEvent()
        {
            FinishedEvent?.Invoke(this, new StatesChangeEventArgs
            {
                Id = Id,
                Winner = PlayersStats.Values.FirstOrDefault(q => q.Health > 0)?.PlayerId,
                Loser = PlayersStats.Values.FirstOrDefault(q => q.Health <= 0)?.PlayerId,
                Key = Key,
                Stats = PlayersStats.Values,
            });
        }

        public Result Begin()
        {
            try
            {
                _tickRateTimer = new Timer(TickRate);

                _idleTimer.Dispose();

                _tickRateTimer.Elapsed += TickRateTimerOnElapsed;

                _tickRateTimer.Start();
            }
            catch (Exception e)
            {
                Dispose();
                return Result.Internal($"Failed to start lobby {Key}. Error: {e.Message}");
            }

            return Result.Ok();
        }

        private void TickRateTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var state in PlayersStats)
            {
                state.Value.Damage(_generator.Next(0, DamageRange));
            }

            if (PlayersStats.Values.Count(q => q.Health <= 0) == 1)
            {
                var deadPlayers = PlayersStats.Values.Select(q => q.Health <= 0)?.ToArray();
                var looser = _generator.Next(0, deadPlayers.Length - 1);

                StopLobby();
            }
            else if (PlayersStats.Values.Count(q => q.Health <= 0) > 1)
            {
                var lastHitPlayers = PlayersStats.Values.Where(q => q.Health <= 0);
            }


            RaiseTickEvent();
        }

        private void StopLobby()
        {
            RaiseFinishedEvent();

            Dispose();
            _tickRateTimer.Dispose();
            _idleTimer.Start();
        }

        public Result<PlayerStats> Join(PlayerProfileDto profile)
        {
            var stats = InitStats(profile);

            if (PlayersStats.Any(q => q.Key.Id == profile.Id))
                return Result<PlayerStats>.Ok(stats);

            if (PlayersStats.Count >= GetMaxPlayersCount(Type))
                return Result<PlayerStats>.Bad("Failed to join");

            var added = PlayersStats.TryAdd(profile, stats);

            if (added && PlayersStats.Count >= GetMaxPlayersCount(Type))
                Begin();

            return added ? Result<PlayerStats>.Ok(stats) : Result<PlayerStats>.Bad("Failed to join");
        }

        public Result Leave(PlayerProfileDto profile)
        {
            return PlayersStats.TryRemove(profile, out var stats)
                ? Result.Ok()
                : Result.Bad($"{profile.NickName}: Error when trying to leave");
        }

        private void IdleTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            _selfDestruct(OwnerId);
        }

        private PlayerStats InitStats(PlayerProfileDto profile)
        {
            return Type switch
            {
                SessionType.BattleRoyale => new PlayerStats(profile.Id, profile.NickName, 10),
                SessionType.DethMatch => new PlayerStats(profile.Id, profile.NickName, 10),
                SessionType.Duel => new PlayerStats(profile.Id, profile.NickName, 10),
                SessionType.CreationMode => new PlayerStats(profile.Id, profile.NickName, int.MaxValue),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static int GetSelfDestructPeriod(SessionType type)
        {
            return type switch
            {
                SessionType.BattleRoyale => 200001,
                SessionType.DethMatch => 200002,
                SessionType.Duel => 200003,
                SessionType.CreationMode => 200004,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private static int GetMaxPlayersCount(SessionType type)
        {
            return type switch
            {
                SessionType.BattleRoyale => 100,
                SessionType.DethMatch => 32,
                SessionType.Duel => 2,
                SessionType.CreationMode => 32,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public void Dispose()
        {
            _idleTimer?.Dispose();
            _tickRateTimer?.Dispose();

            PlayersStats.Clear();

            StartedEvent = null;
            FinishedEvent = null;
            TickHappened = null;
        }
    }
}
