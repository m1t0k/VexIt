using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VexIT.Api.Controllers.V1;
using VexIT.Core.Interfaces;
using VexIT.DataContracts.V1.Business;

namespace VexIT.Tests
{
    [TestClass]
    public class EventsServiceTests : TestBase
    {
        public EventsServiceTests() : base()
        {
        }

        [TestMethod]
        public async Task CrudTests()
        {
            var eventService = ServiceProvider.GetService<IEventsService>();
            Assert.IsNotNull(eventService);

            var eventController = new EventsController(eventService);

            var item = new EventDto()
            {
                Name = "event 1",
                Place = "LN",
                ScheduledAt = DateTime.Today,
                CategoryId = EventCategory.Concert
            };
            var result = await eventController.CreateEvents(item);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.AreEqual(result.Name, item.Name);
            Assert.AreEqual(result.Place, item.Place);
            Assert.AreEqual(result.ScheduledAt, item.ScheduledAt);
            Assert.AreEqual(result.CategoryId, EventCategory.Concert);
        }
    }
}