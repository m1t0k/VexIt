using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VexIT.DataAccess.Db
{
    public static class VexItContextExtension
    {
        private static bool AllMigrationsApplied(this Microsoft.EntityFrameworkCore.DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeedData(this VexItContext context)
        {
            TurnOnSnapshotIsolation(context);
        }


        private static void TurnOnSnapshotIsolation(Microsoft.EntityFrameworkCore.DbContext context)
        {
            var conn = context.Database.GetDbConnection();
            var providerName = conn.GetType().Name;
            if (providerName != "SqlConnection") return;

            conn.Open();
            context.Database.ExecuteSqlCommand(
                $"ALTER DATABASE CURRENT  SET ALLOW_SNAPSHOT_ISOLATION ON;ALTER DATABASE CURRENT SET READ_COMMITTED_SNAPSHOT ON; ");
            conn.Close();
        }
    }
}