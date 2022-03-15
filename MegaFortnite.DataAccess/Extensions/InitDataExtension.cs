using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaFortnite.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MegaFortnite.DataAccess.Extensions
{
    public static class InitDataExtension
    {
        public static ModelBuilder InitData(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .InitProfiles();
        }

        private static ModelBuilder InitProfiles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().HasData(
                new Profile { Id = 1, NickName = "xXx_predator_xXx", CustomerId = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bff"), Created = DateTime.UtcNow },
                new Profile { Id = 2, NickName = "HArU6ATOP", CustomerId = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfa"), Created = DateTime.UtcNow },
                new Profile { Id = 3, NickName = "4TO_C_E6AJIOM", CustomerId = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfb"), Created = DateTime.UtcNow }
                );

            return modelBuilder;
        }
    }
}
