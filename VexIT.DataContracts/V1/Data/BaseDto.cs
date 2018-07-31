using System;
using Newtonsoft.Json;

namespace VexIT.DataContracts.V1.Data
{
    public abstract class BaseDto : IBaseDto
    {
        [JsonProperty] public Guid Id { get; set; }
    }
}