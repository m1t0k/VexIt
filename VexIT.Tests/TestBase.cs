using System;
using System.Data;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VexIT.Core.AutoMapper;
using VexIT.Core.Implementation.Business;
using VexIT.Core.Interfaces;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;
using VexIT.DataAccess.Repositories;

namespace VexIT.Tests
{
    public class TestBase
    {
        protected IMapper Mapper;
        protected VexItContext Context;
        protected ServiceProvider ServiceProvider;
        private readonly DatabaseFixture _fixture = new DatabaseFixture();

        public TestBase()
        {
            Connection = _fixture.Connection;
            Mapper = AutoMapperConfiguration.Mapper;
            var options = new DbContextOptionsBuilder<VexItContext>()
                .UseSqlite(Connection)
                .Options;

            Context = new VexItContext(options);
            Context.Database.Migrate();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(this.Context);


            serviceCollection.AddSingleton(this.Mapper);
            serviceCollection.AddDbContext<VexItContext>(ops => { ops.UseSqlite(Connection); });

            serviceCollection.AddTransient<IRepository<Event>, EventRepository>();
            serviceCollection.AddTransient<IEventsService, EventsService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();

            // sqllite supports only serializable isolation level
            Context.DefaultBatchIsolationLevel = IsolationLevel.Serializable;
            DatabaseFixture.Migrate(ServiceProvider);


        }

        protected SqliteConnection Connection { get; private set; }

        protected string Name => Guid.NewGuid().ToString();

    }
}