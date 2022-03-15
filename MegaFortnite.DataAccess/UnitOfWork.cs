using System;
using System.Threading;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Repositories.Implementations;
using MegaFortnite.DataAccess.Repositories.Interfaces;

namespace MegaFortnite.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MegaFortniteDbContext _db;
        public MegaFortniteDbContext DbContext => _db;

        private readonly Lazy<IProfileRepository> _profiles;
        private readonly Lazy<ISessionRepository> _sessions;
        private readonly Lazy<ISessionResultRepository> _results;

        public IProfileRepository Profiles => _profiles.Value;
        public ISessionRepository Sessions => _sessions.Value;
        public ISessionResultRepository Results => _results.Value;

        public UnitOfWork(MegaFortniteDbContext db)
        {
            _db = db;
            _profiles = new Lazy<IProfileRepository>(() => new ProfileRepository(db));
            _sessions = new Lazy<ISessionRepository>(() => new SessionRepository(db));
            _results = new Lazy<ISessionResultRepository>(() => new SessionResultRepository(db));
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _db.SaveChangesAsync(cancellationToken);
    }
}
