using System.Collections.Generic;
using Newtonsoft.Json;

namespace VexIT.DataContracts.V1.Common
{
    [JsonObject("PagedResult")]
    public class PagedResult<T> where T : class
    {
        public PagedResult()
        {
            TotalCount = 0;
            TotalPages = 0;
        }

        [JsonProperty] public ICollection<T> Items { get; set; }

        [JsonProperty] public int TotalCount { get; set; }

        [JsonProperty] public int TotalPages { get; set; }
    }
}