using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var item = new EventDto()
            {
                Name = "event 1",
                Place = "LN",
                ScheduledAt = DateTime.Today
            };
            var result = await eventService.CreateItemAsync(item);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.AreEqual(result.Name, item.Name);
            Assert.AreEqual(result.Place, item.Place);
            Assert.AreEqual(result.ScheduledAt, item.ScheduledAt);
        }
    }
}