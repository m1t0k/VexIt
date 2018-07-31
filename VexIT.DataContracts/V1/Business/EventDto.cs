using System;
using Newtonsoft.Json;
using VexIT.DataContracts.V1.Data;

namespace VexIT.DataContracts.V1.Business
{
    [JsonObject("Event")]
    public class EventDto : BaseDto
    {
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string Place { get; set; }
        [JsonProperty] public DateTime ScheduledAt { get; set; }
    }
}