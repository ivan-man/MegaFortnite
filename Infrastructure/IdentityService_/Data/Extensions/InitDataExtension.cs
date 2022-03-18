using System;
using IdentityService.Model;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Extensions
{
    public static class InitDataExtension
    {
        public static ModelBuilder InitData(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .InitUsers();
        }

        private static ModelBuilder InitUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bff").ToString(),
                    UserName = "user1", NormalizedUserName = "USER1",
                    Email = "test@test.com",
                    NormalizedEmail = "TEST@TEST.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEPmEMcv7dFyDQF3Cmvv6ygk97CB0qyDHr9POnFEd+m2Ac6iCfQhLrZId8Xu/6pAp+A==",
                    SecurityStamp = "QSUPLKBJTMZADLY4BZLNOCUUII7EYQ4O",
                    ConcurrencyStamp = "46c2931a-f566-4cb9-a034-e7526e0c3280",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "TestFirstName",
                    LastName = "TestLastName",
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfa").ToString(),
                    UserName = "user2",
                    NormalizedUserName = "USER2",
                    Email = "test_2@test.com",
                    NormalizedEmail = "TEST_2@TEST.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEPmEMcv7dFyDQF3Cmvv6ygk97CB0qyDHr9POnFEd+m2Ac6iCfQhLrZId8Xu/6pAp+A==",
                    SecurityStamp = "QSUPLKBJTMZADLY4BZLNOCUUII7EYQ4O",
                    ConcurrencyStamp = "46c2931a-f566-4cb9-a034-e7526e0c328a",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "Second",
                    LastName = "Second",
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("a6b3ee91-1be7-4eab-a15b-7bffc8b94bfb").ToString(),
                    UserName = "user3",
                    NormalizedUserName = "USER3",
                    Email = "test_3@test.com",
                    NormalizedEmail = "TEST_3@TEST.COM",
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEPmEMcv7dFyDQF3Cmvv6ygk97CB0qyDHr9POnFEd+m2Ac6iCfQhLrZId8Xu/6pAp+A==",
                    SecurityStamp = "QSUPLKBJTMZADLY4BZLNOCUUII7EYQ4O",
                    ConcurrencyStamp = "46c2931a-f566-4cb9-a034-e7526e0c328b",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "third",
                    LastName = "third",
                }
            );

            return modelBuilder;
        }
    }
}
