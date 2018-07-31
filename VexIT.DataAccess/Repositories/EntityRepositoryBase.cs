using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;

namespace VexIT.DataAccess.Repositories
{
    public class EntityRepositoryBase<TEntity> : RepositoryBase, IEntityRepositoryBase<TEntity>
        where TEntity : EntityBase, IEntity
    {
        public EntityRepositoryBase(VexItContext context)
            : base(context)
        {
        }


        /// <summary>
        ///     Gets the underlying DbSet.
        /// </summary>
        public virtual DbSet<TEntity> Data => throw new NotImplementedException();

        public virtual void CheckMaxLengths(TEntity entity)
        {
        }

        /// <summary>
        ///     Returns a record count.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync()
        {
            return await GetData().CountAsync();
        }

        /// <summary>
        ///     Creates an existing entity in the data store.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            using (var dbContextTransaction = Context.Database.BeginTransaction(Context.DefaultBatchIsolationLevel))
            {
                try
                {
                    entity.Id = Guid.NewGuid();

                    Data.Add(entity);

                    await OnCreate(entity);

                    Context.ChangeTracker.DetectChanges();
                    await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    await OnCreated(entity);


                    return await GetAsync(entity.Id);
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }


        /// <summary>
        ///     Creates an existing entity in the data store.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CreateAsync(IEnumerable<TEntity> entityList)
        {
            var list = entityList?.ToList();
            if (list == null || list.Count <= 0)
                return 0;

            using (var dbContextTransaction = Context.Database.BeginTransaction(Context.DefaultBatchIsolationLevel))
            {
                try
                {
                    foreach (var entity in list)
                    {
                        entity.Id = Guid.NewGuid();
                        Data.Add(entity);
                        await OnCreate(entity);
                    }

                    Context.ChangeTracker.DetectChanges();
                    var result = await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    foreach (var entity in list)
                        await OnCreated(entity);

                    return result;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }

        /// <summary>
        ///     Deletes an existing entity from the data store.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            using (var dbContextTransaction = Context.Database.BeginTransaction(Context.DefaultBatchIsolationLevel))
            {
                try
                {
                    var entity = await GetAsync(id);

                    await OnDelete(entity);

                    Data.Remove(entity);
                    var result = await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    await OnDeleted(entity);

                    return result;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    throw ex;
                }
            }
        }


        /// <summary>
        ///     Creates an existing entity in the data store.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(IEnumerable<TEntity> entityList)
        {
            var list = entityList?.ToList();
            if (list == null || list.Count <= 0)
                return 0;

            using (var dbContextTransaction = Context.Database.BeginTransaction(Context.DefaultBatchIsolationLevel))
            {
                try
                {
                    foreach (var entity in list) Data.Remove(entity);

                    Context.ChangeTracker.DetectChanges();
                    var result = await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        ///     Gets a paged list of entities.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize">Set to zero to turn off paging.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(int page, int pageSize)
        {
            page = SafePage(page);
            pageSize = SafePageSize(pageSize);

            if (pageSize == 0) return await GetData().ToListAsync();

            var skip = CalculateSkip(page, pageSize);

            return await GetData()
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        ///     Gets a single entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Guid id, bool noTracking = false)
        {
            var query = Include();

            if (noTracking) query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        ///     Returns an IQueryable.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetData()
        {
            return Include();
        }

        /// <summary>
        ///     Gets the DbSet as IQueryable.  Override this virtual method if you need to include data related data entities in
        ///     the result set.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Include()
        {
            return Data.AsQueryable();
        }

        /// <summary>
        ///     Called before changes are saved.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task OnCreate(TEntity entity)
        {
            CheckMaxLengths(entity);
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called after changes are saved.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task OnCreated(TEntity entity)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called before the existing entity is deleted.
        /// </summary>
        /// <param name="id">The id of the existing entity to delete.</param>
        /// <returns></returns>
        public virtual Task OnDelete(TEntity entity)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called after the existing entity is deleted.
        /// </summary>
        /// <param name="id">The id of the existing entity deleted.</param>
        /// <returns></returns>
        public virtual Task OnDeleted(TEntity entity)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called before changes are saved.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task OnUpdate(TEntity entity)
        {
            CheckMaxLengths(entity);
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Called after changes are saved.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task OnUpdated(TEntity entity)
        {
            return Task.FromResult(true);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            using (var dbContextTransaction = Context.Database.BeginTransaction(Context.DefaultBatchIsolationLevel))
            {
                try
                {
                    Context.ChangeTracker.DetectChanges();
                    await OnUpdate(entity);

                    var result = await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();

                    await OnUpdated(entity);

                    return result;
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        ///     Caclulates how many records to skip based on the page and page size.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected int CalculateSkip(int page, int pageSize)
        {
            page = SafePage(page);
            pageSize = SafePageSize(pageSize);

            return (page - 1) * pageSize;
        }

        /// <summary>
        ///     Returns a valid page value.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected int SafePage(int page)
        {
            if (page < 1) page = 1;

            return page;
        }

        /// <summary>
        ///     Returns a valid page size value.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        protected int SafePageSize(int pageSize)
        {
            if (pageSize < 0) pageSize = 10;

            if (pageSize > 100) pageSize = 100;

            return pageSize;
        }
    }
}