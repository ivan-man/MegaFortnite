using System;
using System.Collections.Concurrent;
using MediatR;
using MegaFortnite.Business.SendLobbyNotification;
using MegaFortnite.Common.Enums;
using MegaFortnite.Common.Helpers;
using MegaFortnite.Common.Result;
using MegaFortnite.Engine;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace MegaFortnite.Business.InternalServices
{
    public class LobbyManager : ILobbyManager
    {
        private readonly ILogger<LobbyManager> _logger;
        private readonly IMediator _mediator;

        private readonly MemoryCache _memoryCache = new(new MemoryCacheOptions());
        //private readonly IUnitOfWork _unitOfWork;

        private readonly ConcurrentDictionary<string, Lobby> _actualLobbies = new();
        private readonly ConcurrentDictionary<Guid, Lobby> _owners = new();

        public LobbyManager(IMediator mediator, ILogger<LobbyManager> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Result<Lobby> GetLobby(string key)
        {
            return _actualLobbies.TryGetValue(key, out var lobby) ? Result<Lobby>.Ok(lobby) : Result<Lobby>.NotFound();
        }

        public Result<Lobby> CreateLobby(Guid customerId, SessionType sessionType)
        {
            if (customerId == default)
                return Result<Lobby>.Bad("Invalid UserId");

            if (_owners.ContainsKey(customerId))
                return Result<Lobby>.Ok(_owners[customerId]);

            string key;
            do
            {
                key = LobbyKeyGenerator.GenerateKey(5);
            } while (_actualLobbies.ContainsKey(key));

            var lobby = new Lobby(customerId, sessionType, key, Remove);

            if (!_owners.TryAdd(customerId, lobby))
            {
                _logger.LogWarning("Failed to add key into pool. {OwnerId} {SessionType} {Key}",
                    customerId, sessionType, key);
                return Result<Lobby>.Internal();
            }


            lobby.FinishedEvent += LobbyOnFinishedEvent;

            if (_actualLobbies.TryAdd(key, lobby))
                return Result<Lobby>.Ok(lobby);

            _logger.LogWarning("Failed to add lobby into pool. {OwnerId} {SessionType} {Key}",
                customerId, sessionType, key);

            return Result<Lobby>.Internal();
        }

        public Result SetOrUpdateConnectionId(Guid customerId, string connectionId)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(600),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromSeconds(300)
            };

            _memoryCache.Set(customerId, connectionId, cacheExpiryOptions);

            return Result.Ok();
        }

        public Result<string> GetConnectionId(Guid customerId)
        {
            if (!_memoryCache.TryGetValue(customerId, out string connectionId))
            {
                return Result<string>.NotFound();
            }

            return Result<string>.Ok(connectionId);
        }

        //Говнокод
        private async void LobbyOnFinishedEvent(object sender, StatesChangeEventArgs e)
        {
            await _mediator.Send(new SendLobbyNotificationCommand
            {
                Message = "срака",
                LobbyKey = e.Key,
            });
            //ToDO не работает
            //if (!e.Loser.HasValue || !e.Winner.HasValue || e.Id == default)
            //{
            //    _logger.LogWarning("Invalid game result {@Result}", e);
            //    return;
            //}

            //var session = await _unitOfWork.Sessions.GetAsync(q => q.Id == e.Id, cancellationToken: CancellationToken.None);
            //if (session is not null)
            //    session.State = SessionState.Finished;

            //var winnerResult = new SessionResult
            //{
            //    GameProfileId = e.Winner ?? 0,
            //    Score = 1,
            //    SessionId = e.Id
            //};

            //var loserResult = new SessionResult
            //{
            //    GameProfileId = e.Loser ?? 0,
            //    Score = -1,
            //    SessionId = e.Id
            //};

            //var loser = await _unitOfWork.Profiles.GetAsync(q => q.Id == e.Loser);
            //if (loser is not null)
            //    loser.Rate--;

            //var winner = await _unitOfWork.Profiles.GetAsync(q => q.Id == e.Winner);
            //if (winner is not null)
            //    winner.Rate++;

            //ToDO не работает
            //await _unitOfWork.SaveChangesAsync(CancellationToken.None);

            //await _mediator.Send(new LobbyFinishedCommand
            //{
            //    Loser = e.Loser ?? 0,
            //    Winner = e.Winner ?? 0,
            //    Id = e.Id,
            //});
        }

        public Result Remove(Guid ownerId)
        {
            if (ownerId == default)
                return Result<Lobby>.Bad("Invalid UserId");

            if (!_owners.ContainsKey(ownerId))
                return Result<Lobby>.Bad("User already created lobby");

            if (!_owners.TryRemove(ownerId, out var lobby))
            {
                _logger.LogWarning("Lobby of {OwnerId} not found.", ownerId);
                return Result.NotFound("Lobby of not found.");
            }

            if (!_actualLobbies.TryRemove(lobby.Key, out var removedLobby))
            {
                _logger.LogWarning("Lobby of {OwnerId} with {Key} not found.", ownerId, lobby.Key);
                return Result.NotFound("Lobby of not found.");
            }

            lobby.Dispose();

            return Result.Ok();
        }
    }
}
