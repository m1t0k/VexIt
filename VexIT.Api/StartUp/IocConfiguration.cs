﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VexIT.Core.AutoMapper;
using VexIT.Core.Implementation.Business;
using VexIT.Core.Interfaces;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;
using VexIT.DataAccess.Repositories;

namespace VexIT.Api.StartUp
{
    /// <summary>
    /// </summary>
    public class IocConfiguration
    {
        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton(sp => AutoMapperConfiguration.Mapper);
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IRepository<Event>, EventRepository>();
        }
    }
}