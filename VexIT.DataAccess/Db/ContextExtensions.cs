using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VexIT.DataAccess.Db
{
    public static class ContextExtensions
    {
        public static void AddOrUpdate(this Microsoft.EntityFrameworkCore.DbContext ctx, object entity)
        {
            var entry = ctx.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    ctx.Add(entity);
                    break;
                case EntityState.Modified:
                    ctx.Update(entity);
                    break;
                case EntityState.Added:
                    ctx.Add(entity);
                    break;
                case EntityState.Unchanged:
                    //item already in db no need to do anything  
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void AddOrUpdate(this Microsoft.EntityFrameworkCore.DbContext ctx, IEnumerable<object> entityList)
        {
            foreach (var entry in entityList)
                AddOrUpdate(ctx, entry);
        }
    }
}