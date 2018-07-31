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
        public IsolationLevel DefaultBatchIsolationLevel { get; set; } = IsolationLevel.Snapshot;

        public DbSet<Event> Events { get; set; }
    }
}