using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Repositories.Interfaces;

namespace MegaFortnite.DataAccess
{
    public interface IUnitOfWork
    {
        MegaFortniteDbContext DbContext { get; }
        ICustomerRepository Customers { get; }
        IProfileRepository Profiles { get; }
        ISessionRepository Sessions { get; }
        ISessionResultRepository Results { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
