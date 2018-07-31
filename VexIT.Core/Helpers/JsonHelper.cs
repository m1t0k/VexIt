using System;
using System.Linq;
using Newtonsoft.Json;

namespace VexIT.Core.Helpers
{
    public class JsonHelper
    {
        public static JsonPropertyMapping GetJsonPropertyMapping(Type type)
        {
            var mapping = new JsonPropertyMapping();
            foreach (var prop in type.GetProperties())
            {
                var attrList = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), true);
                if (!attrList.Any())
                    continue;
                var attr = attrList.FirstOrDefault() as JsonPropertyAttribute;
                if (attr == null)
                    continue;
                mapping.Mapping.Add(new PropertyMapping {PropertyName = prop.Name, AttributeName = attr.PropertyName});
            }

            return mapping;
        }
    }
}