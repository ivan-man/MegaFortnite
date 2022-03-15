using MegaFortnite.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaFortnite.DataAccess.Extensions;
using MegaFortnite.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MegaFortnite.DataAccess
{
    public sealed class MegaFortniteDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionResult> Results { get; set; }

        public MegaFortniteDbContext(DbContextOptions<MegaFortniteDbContext> options) : base(options)
        {
            ChangeTracker.StateChanged += OnEntityStateChanged;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>()
                .HasIndex(x => x.CustomerId)
                .IsUnique();

            modelBuilder.Entity<Session>()
                .HasMany(x => x.Results);

            modelBuilder.Entity<Session>()
                .HasOne(x => x.Owner);

            modelBuilder.Entity<SessionResult>()
                .HasOne(x => x.GameProfile);

            modelBuilder.Entity<SessionResult>()
                .HasIndex(q => q.GameProfileId);

            modelBuilder.Entity<SessionResult>()
                .HasIndex(q => q.SessionId);

            modelBuilder.Entity<SessionResult>()
                .HasKey(q => new { q.SessionId , q.GameProfileId});

            modelBuilder.Entity<SessionResult>()
                .HasOne(x => x.Session);

            modelBuilder.InitData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        }

        private void OnEntityStateChanged(object sender, EntityStateChangedEventArgs e)
        {
            if (e.Entry.Entity is not IBaseEntity entity || e.Entry.State != EntityState.Modified)
                return;

            entity.Updated = DateTime.UtcNow;
        }
    }
}
