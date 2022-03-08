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
                .InitCustomers()
                .InitProfiles();
        }

        private static ModelBuilder InitCustomers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Phone = "", Email = "test1@test.com", FirstName = "Customer_1", Created = DateTime.UtcNow },
                new Customer { Id = 2, Phone = "", Email = "test2@test.com", FirstName = "Customer_2", Created = DateTime.UtcNow },
                new Customer { Id = 3, Phone = "", Email = "test3@test.com", FirstName = "Customer_3", Created = DateTime.UtcNow }
                );

            return modelBuilder;
        }

        private static ModelBuilder InitProfiles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().HasData(
                new Profile { Id = 1, NickName = "xXx_predator_xXx", CustomerId = 1, Created = DateTime.UtcNow },
                new Profile { Id = 2, NickName = "HArU6ATOP", CustomerId = 1, Created = DateTime.UtcNow },
                new Profile { Id = 3, NickName = "4TO_C_E6AJIOM", CustomerId = 1, Created = DateTime.UtcNow }
                );

            return modelBuilder;
        }
    }
}
