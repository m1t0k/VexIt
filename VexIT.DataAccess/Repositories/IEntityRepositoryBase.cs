using System.Linq;
using System.Threading.Tasks;
using VexIT.DataAccess.Model;

namespace VexIT.DataAccess.Repositories
{
    public interface IEntityRepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        void CheckMaxLengths(T entity);

        IQueryable<T> GetData();

        Task OnCreate(T entity);

        Task OnCreated(T entity);

        Task OnDelete(T entity);

        Task OnDeleted(T entity);

        Task OnUpdate(T entity);

        Task OnUpdated(T entity);
    }
}