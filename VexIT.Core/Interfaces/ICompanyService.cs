using System;
using System.Threading.Tasks;
using VexIT.DataContracts.V1.Business;

namespace VexIT.Core.Interfaces
{
    public interface IEventsService : IBaseDataService<EventDto, EventQueryDto>
    {
    }
}