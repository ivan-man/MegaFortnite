using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Repositories.Interfaces;
using MegaFortnite.Domain.Models;

namespace MegaFortnite.DataAccess.Repositories.Implementations
{
    internal class SessionRepository : BaseRepository<Session, Guid>, ISessionRepository
    {
        internal SessionRepository(MegaFortniteDbContext context) : base(context)
        { }
    }
}
