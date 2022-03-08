using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Extensions;
using MegaFortnite.DataAccess.Repositories.Interfaces;
using MegaFortnite.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaFortnite.DataAccess.Repositories.Implementations
{
    internal class BaseRepository<T, TId> : BaseRepository<T>, IBaseRepository<T, TId> where T : BaseEntity<TId>
    {
        protected BaseRepository(MegaFortniteDbContext context)  : base(context)
        {}
    }

    internal class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected MegaFortniteDbContext Context { get; }

        protected BaseRepository(MegaFortniteDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> GetQuery()
        {
            return Context.Set<T>();
        }

        /// <inheritdoc cref="IBaseRepository{T,TId}.GetAsync"/>>
        public Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByProperty = null,
            bool orderByDescending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] include
        )
        {
            IQueryable<T> query = Context.Set<T>();
            if (include != null && include.Any())
                query = include.Aggregate(query, (current, inc) => current.Include(inc));

            orderByProperty ??= x => x.Created;

            return query.OrderBy(orderByProperty, orderByDescending)
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <inheritdoc cref="IBaseRepository{T,TId}.GetAsync"/>>
        public Task<TSelector> GetAsync<TSelector>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TSelector>> selector,
            Expression<Func<T, object>> orderByProperty = null,
            bool orderByDescending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] include
        )
        {
            IQueryable<T> query = Context.Set<T>();

            if (include != null && include.Any())
                query = include.Aggregate(query, (current, inc) => current.Include(inc));

            orderByProperty ??= x => x.Created;

            return query.OrderBy(orderByProperty, orderByDescending)
                .Where(predicate)
                .Select(selector)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual void Add(T entity) => Context.Set<T>().Add(entity);

        public virtual void Remove(T entity) => Context.Set<T>().Remove(entity);

        public virtual Task<int> CountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Context.Set<T>().CountAsync(predicate, cancellationToken);

        public virtual Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
            => Context.Set<T>().AnyAsync(predicate, cancellationToken);
    }
}
