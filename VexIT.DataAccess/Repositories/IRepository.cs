using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;

namespace VexIT.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        VexItContext Context { get; }

        DbSet<TEntity> Data { get; }

        Task<int> CountAsync();

        Task<TEntity> CreateAsync(TEntity value);
        Task<int> CreateAsync(IEnumerable<TEntity> value);

        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(IEnumerable<TEntity> list);

        Task<IEnumerable<TEntity>> GetAsync(int page, int pageSize);

        Task<TEntity> GetAsync(Guid id, bool noTracking = false);

        IQueryable<TEntity> Include();

        Task<int> UpdateAsync(TEntity value);
    }
}