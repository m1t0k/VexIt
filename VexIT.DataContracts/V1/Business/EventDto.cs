using System;
using Newtonsoft.Json;
using VexIT.DataContracts.V1.Data;

namespace VexIT.DataContracts.V1.Business
{
    [JsonObject("Event")]
    public class EventDto : BaseDto
    {
        [JsonProperty] public string Name { get; set; }
        [JsonProperty] public string Country { get; set; }
        [JsonProperty] public string City { get; set; }
        [JsonProperty] public string Street { get; set; }
        [JsonProperty] public string Place { get; set; }
        [JsonProperty] public string YouTubeUrl { get; set; }
        [JsonProperty] public string Description { get; set; }
        [JsonProperty] public DateTime ScheduledAt { get; set; }
        [JsonProperty] public EventCategory CategoryId { get; set; }
        [JsonProperty] public PhotoDto Photo { get; set; }
    }

    [JsonObject("Photo")]
    public class PhotoDto : BaseDto
    {
        [JsonProperty] public string Content { get; set; }
    }
}