using MegaFortnite.DataAccess.Repositories.Interfaces;
using MegaFortnite.Domain.Models;

namespace MegaFortnite.DataAccess.Repositories.Implementations
{
    internal class ProfileRepository : BaseRepository<Profile, int>, IProfileRepository
    {
        internal ProfileRepository(MegaFortniteDbContext context) : base(context)
        { }
    }
}
