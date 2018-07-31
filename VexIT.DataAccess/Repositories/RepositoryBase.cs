using VexIT.DataAccess.Db;

namespace VexIT.DataAccess.Repositories
{
    public abstract class RepositoryBase
    {
        protected RepositoryBase(VexItContext context)
        {
            Context = context;
        }

        public VexItContext Context { get; }
    }
}