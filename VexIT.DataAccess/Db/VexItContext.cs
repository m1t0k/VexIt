using System.Data;
using Microsoft.EntityFrameworkCore;
using VexIT.DataAccess.Model;

namespace VexIT.DataAccess.Db
{
    public class VexItContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public VexItContext(DbContextOptions<VexItContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>()
                .HasOne(p => p.Photo)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public IsolationLevel DefaultBatchIsolationLevel { get; set; } = IsolationLevel.Snapshot;

        public DbSet<Event> Events { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}