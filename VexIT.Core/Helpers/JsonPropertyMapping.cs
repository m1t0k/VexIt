using System.Collections.Generic;

namespace VexIT.Core.Helpers
{
    public class JsonPropertyMapping
    {
        public JsonPropertyMapping()
        {
            Mapping = new List<PropertyMapping>();
        }

        public List<PropertyMapping> Mapping { get; set; }
    }
}