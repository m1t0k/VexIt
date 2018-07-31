using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VexIT.DataAccess.Db;

namespace VexIT.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
        }

        public SqliteConnection Connection { get; private set; }

        public void Dispose()
        {
            Connection.Close();
        }

        public static void Migrate(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<VexItContext>();
            context.Database.Migrate();
            context.EnsureSeedData();
        }
    }
}