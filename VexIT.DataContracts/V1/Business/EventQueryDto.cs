using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace VexIT.DataContracts.V1.Business
{
    [JsonObject("EventQuery")]
    public class EventQueryDto
    {
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string Place { get; set; }
        [JsonProperty] public DateTime? ScheduledAt { get; set; }
    }
}