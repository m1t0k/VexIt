using System;
using Newtonsoft.Json;

namespace VexIT.DataContracts.V1.Common
{
    [JsonObject("Response")]
    public class ResponseDto
    {
        [JsonProperty] public Guid? Id { get; set; }

        [JsonProperty] public bool Success { get; set; }

        [JsonProperty] public string Message { get; set; }
    }
}