using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using VexIT.Core.Interfaces;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;
using VexIT.DataAccess.Repositories;
using VexIT.DataContracts.V1.Common;
using VexIT.DataContracts.V1.Data;

namespace VexIT.Core.Implementation.Base
{
    public abstract class BaseDataService<T, TU, TV> : IBaseDataService<TU, TV>
        where T : EntityBase where TU : BaseDto where TV : class

    {
        private const int PageSize = 20;
        protected VexItContext Context;
        protected IMapper Mapper;
        protected IRepository<T> Repository;

        protected BaseDataService(IMapper mapper, VexItContext context)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<TU> CreateItemAsync(TU dtoItem)
        {
            if (dtoItem == null)
                throw new ArgumentNullException(nameof(dtoItem));

            await ValidateItem(dtoItem);
            var dbItem = Mapper.Map<T>(dtoItem);
            var result = await Repository.CreateAsync(dbItem);

            return Mapper.Map<TU>(result);
        }

        public virtual async Task DeleteItemAsync(Guid id)
        {
            await Repository.DeleteAsync(id);
        }


        public virtual async Task<TU> GetItemAsync(Guid id)
        {
            var dbItem = await Repository.GetAsync(id, true);
            return Mapper.Map<TU>(dbItem);
        }

        public virtual async Task<DataContracts.V1.Common.PagedResult<TU>> GetItemsAsync(int? pageIndex,
            int? pageSize, string orderBy)
        {
            return await GetDbItemsAsync(pageIndex, pageSize, orderBy);
        }

        public virtual async Task<DataContracts.V1.Common.PagedResult<TU>> SearchItemsAsync(TV queryRequest,
            int? pageIndex, int? pageSize, string orderBy)
        {
            var query = BuildSearchQuery(queryRequest);
            return await GetDbItemsAsync(pageIndex, pageSize, orderBy, query.AsNoTracking());
        }

        public virtual async Task UpdateItemAsync(Guid id, TU dtoItem)
        {
            if (dtoItem == null)
                throw new ArgumentNullException(nameof(dtoItem));

            await ValidateItem(dtoItem);
            var dbItem = await Repository.GetAsync(id);
            dbItem = Mapper.Map(dtoItem, dbItem);
            await Repository.UpdateAsync(dbItem);
        }
        

        protected abstract IQueryable<T> BuildSearchQuery(TV searchQuery);

        protected async Task<DataContracts.V1.Common.PagedResult<TU>> GetDbItemsAsync(int? pageIndex,
            int? pageSize, string orderBy, IQueryable<T> baseQuery = null)
        {
            var itemSet = baseQuery ?? Repository.Data.AsQueryable();

            if (!pageSize.HasValue || pageSize.Value <= 0)
                pageSize = PageSize;

            orderBy = GetOrderByClause(orderBy);
            var query = itemSet.OrderBy(orderBy).AsQueryable();
            if (pageIndex.HasValue && pageIndex.Value > 0)
                query = query.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);

            var list = await query.ToListAsync();

            return DataServiceHelper.BuildPagedResult(Mapper.Map<List<TU>>(list), itemSet.Count(), pageSize.Value);
        }

        protected virtual string GetDefaultSorting()
        {
            return "Id asc";
        }

        protected string GetOrderByClause(string orderByIn)
        {
            if (!string.IsNullOrWhiteSpace(orderByIn) && orderByIn.Split(new[] {'.'}).Length > 1)
                return orderByIn;

            return DataServiceHelper.BuildOrderByClause<T, TU>(orderByIn, GetDefaultSorting(), Mapper);
        }
        
        protected virtual async Task ValidateItem(TU dtoItem)
        {
            await Task.CompletedTask;
        }
    }
}