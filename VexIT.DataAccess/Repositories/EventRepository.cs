using Microsoft.EntityFrameworkCore;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;

namespace VexIT.DataAccess.Repositories
{
    public class EventRepository : EntityRepositoryBase<Event>
    {
        public EventRepository(VexItContext context):base(context)
        {
        }

        public override DbSet<Event> Data => Context.Events;
    }
}