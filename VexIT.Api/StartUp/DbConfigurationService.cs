using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VexIT.DataAccess.Db;

namespace VexIT.Api.StartUp
{
    /// <summary>
    /// </summary>
    public static class DbConfigurationService
    {
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public static IWebHost Migrate(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                Migrate(scope);
            }

            return webhost;
        }

        public static void Migrate(IServiceProvider provider)
        {
            using (var serviceScope = provider.GetService<IServiceScopeFactory>().CreateScope())
            {
                Migrate(serviceScope);
            }
        }

        private static void Migrate(IServiceScope serviceScope)
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<VexItContext>();
            context.Database.Migrate();
            context.EnsureSeedData();
        }
    }
}