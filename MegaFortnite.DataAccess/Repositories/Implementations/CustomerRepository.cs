using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Repositories.Interfaces;
using MegaFortnite.Domain.Models;

namespace MegaFortnite.DataAccess.Repositories.Implementations
{
    internal class CustomerRepository : BaseRepository<Customer, int>, ICustomerRepository
    {
        internal CustomerRepository(MegaFortniteDbContext context) : base(context)
        { }
    }
}
