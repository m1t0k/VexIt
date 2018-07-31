using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VexIT.Core.Interfaces;
using VexIT.DataContracts.V1.Business;
using VexIT.DataContracts.V1.Common;

namespace VexIT.Api.Controllers.V1
{
    [Route("api/events")]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/events")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventsService _service;

        /// <summary>
        ///     Endpoint for managing Event entities.
        /// </summary>
        public EventsController(IEventsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #region Event

        /// <summary>
        ///     Method creates Event entity.
        /// </summary>
        /// <param name="item">Event item.</param>
        /// <response code="200">Entity is successfully created.</response>
        /// <response code="400">Request is invalid.</response>
        /// <response code="500">Error occured.</response>
        [HttpPost("")]
        [ProducesResponseType(typeof(EventDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        public async Task<EventDto> CreateEvents([FromBody] EventDto item)
        {
            return await _service.CreateItemAsync(item);
        }

        /// <summary>
        ///     Method deletes selected Event entity.
        /// </summary>
        /// <param name="id">Event entity id.</param>
        /// <response code="200">Entity is successfully deleted.</response>
        /// <response code="404">Entity doesn't exist.</response>
        /// <response code="500">Error occured.</response>
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        [HttpDelete("{id}")]
        public async Task<ResponseDto> DeleteEvent(Guid id)
        {
            await _service.DeleteItemAsync(id);
            return new ResponseDto {Success = true, Id = id};
        }

        /// <summary>
        ///     Method returns Event entity indentified by dd.
        /// </summary>
        /// <produces>application/json</produces>
        /// <param name="id">Event id.</param>
        /// <response code="200">Returns selected card.</response>
        /// <response code="404">Entity doesn't exist.</response>
        /// <response code="500">Error occured.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        public async Task<EventDto> GetEvent(Guid id)
        {
            return await _service.GetItemAsync(id);
        }

        /// <summary>
        ///     Method returns full list of Event enties. Method has paging and sorting support.
        /// </summary>
        /// <param name="pageIndex">The index of page which should be returned. Index is starting from 1. Optional.</param>
        /// <param name="pageSize">Page size. Number of items in the page.  Optional.</param>
        /// <param name="orderBy">Sort by expression in SQL like manner: 'Name' or 'Name desc'. Optional.</param>
        /// <response code="200" type="application/json">Returns list of Event enties.</response>
        /// <response code="500">Error occured.</response>
        [ProducesResponseType(typeof(PagedResult<EventDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        [HttpGet]
        public async Task<PagedResult<EventDto>> GetEvents(int? pageIndex,
            int? pageSize, string orderBy = "")
        {
            return await _service.GetItemsAsync(pageIndex, pageSize, orderBy);
        }

        /// <summary>
        ///     Method search for list of Event enties. Method has paging and sorting support.
        /// </summary>
        /// <param name="query">Query pattern.</param>
        /// <param name="pageIndex">The index of page which should be returned. Index is starting from 1. Optional.</param>
        /// <param name="pageSize">Page size. Number of items in the page. Default value is 20. Optional.</param>
        /// <param name="orderBy">Sort by expression in SQL like manner: 'Name' or 'Name desc'. Optional.</param>
        /// <response code="200" type="application/json">Returns list of Event enties.</response>
        /// <response code="500">Error occured.</response>
        [ProducesResponseType(typeof(PagedResult<EventDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        [HttpPost("search")]
        public async Task<PagedResult<EventDto>> SearchEvents(
            [FromBody] EventQueryDto query, [FromQuery] int? pageIndex,
            [FromQuery] int? pageSize, [FromQuery] string orderBy = "")
        {
            return await _service.SearchItemsAsync(query, pageIndex, pageSize, orderBy);
        }


        /// <summary>
        ///     Method updates selected Event.
        /// </summary>
        /// <param name="id">Event entity id.</param>
        /// <param name="item">Event item.</param>
        /// <response code="200">Entity is successfully updated.</response>
        /// <response code="400">Request is invalid.</response>
        /// <response code="404">Entity doesn't exist.</response>
        /// <response code="500">Error occured.</response>
        [ProducesResponseType(typeof(ResponseDto), 200)]
        [ProducesResponseType(typeof(ResponseDto), 400)]
        [ProducesResponseType(typeof(ResponseDto), 404)]
        [ProducesResponseType(typeof(ResponseDto), 500)]
        [HttpPut("{id}")]
        public async Task<ResponseDto> UpdateEvent(Guid id, [FromBody] EventDto item)
        {
            await _service.UpdateItemAsync(id, item);
            return new ResponseDto {Success = true, Id = id};
        }

        #endregion Event    
    }
}