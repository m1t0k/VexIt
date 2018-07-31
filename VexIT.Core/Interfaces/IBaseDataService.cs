using System;
using System.Threading.Tasks;
using VexIT.DataContracts.V1.Common;
using VexIT.DataContracts.V1.Data;

namespace VexIT.Core.Interfaces
{
    public interface IBaseDataService<TU, TS> where TU : BaseDto where TS : class
    {
        Task<PagedResult<TU>> GetItemsAsync(int? pageIndex, int? pageSize, string orderBy);
        Task<PagedResult<TU>> SearchItemsAsync(TS query, int? pageIndex, int? pageSize, string orderBy);
        Task<TU> GetItemAsync(Guid id);
        Task<TU> CreateItemAsync(TU dtoItem);
        Task UpdateItemAsync(Guid id, TU dtoItem);
        Task DeleteItemAsync(Guid id);
    }
}