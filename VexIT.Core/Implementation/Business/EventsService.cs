using System.Linq;
using AutoMapper;
using VexIT.Core.Implementation.Base;
using VexIT.Core.Interfaces;
using VexIT.DataAccess.Db;
using VexIT.DataAccess.Model;
using VexIT.DataAccess.Repositories;
using VexIT.DataContracts.V1.Business;

namespace VexIT.Core.Implementation.Business
{
    public class EventsService : BaseDataService<Event, EventDto,
        EventQueryDto>, IEventsService
    {
        public EventsService(IMapper mapper, VexItContext context) : base(mapper, context)
        {
            Repository = new EventRepository(context);
        }


        protected override IQueryable<Event> BuildSearchQuery(EventQueryDto searchQuery)
        {
            return Context.Events;
        }
    }
}