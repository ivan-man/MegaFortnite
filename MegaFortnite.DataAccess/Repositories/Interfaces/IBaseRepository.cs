using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MegaFortnite.Domain.Models;

namespace MegaFortnite.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T, TId> : IBaseRepository<T> where T : BaseEntity<TId>
    {
        //Methods with Id if it needed
    }

    public interface IBaseRepository<T> where T : IBaseEntity 
    {
        public IQueryable<T> GetQuery();

        /// <param name="predicate">Filter</param>
        /// <param name="orderByProperty">Default value: Created</param>
        /// <param name="orderByDescending">Default value:true</param>
        /// <param name="cancellationToken"></param>
        /// <param name="include">Example: p => p.AccountChart, p => p.Currency</param>
        /// <returns>Entity or nothing</returns>
        public Task<T> GetAsync(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderByProperty = null,
            bool orderByDescending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] include);

        /// <param name="predicate">Filter</param>
        /// <param name="selector">Selector to select several properties</param>
        /// <param name="orderByProperty">Default value: Created</param>
        /// <param name="orderByDescending">Default value:true</param>
        /// <param name="cancellationToken"></param>
        /// <param name="include">Example: p => p.AccountChart, p => p.Currency</param>
        /// <returns>Entity or nothing</returns>
        public Task<TSelector> GetAsync<TSelector>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TSelector>> selector,
            Expression<Func<T, object>> orderByProperty = null,
            bool orderByDescending = true,
            CancellationToken cancellationToken = default,
            params Expression<Func<T, object>>[] include);

        public void Add(T entity);
        public void Remove(T entity);

        public Task<int> CountAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        public Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);
    }
}
