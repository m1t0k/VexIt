using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using VexIT.Core.Helpers;
using VexIT.DataContracts.V1.Common;

namespace VexIT.Core.Implementation.Base
{
    public static class DataServiceHelper
    {
        static DataServiceHelper()
        {
            JsonPropertyMappings = new ConcurrentDictionary<string, JsonPropertyMapping>();
        }

        private static ConcurrentDictionary<string, JsonPropertyMapping> JsonPropertyMappings { get; }


        public static string BuildOrderByClause<T, TU>(string orderByIn, string defaultSorting, IMapper mapper)
        {
            var orderBy = defaultSorting;
            if (string.IsNullOrWhiteSpace(orderByIn))
                return orderBy;

            var sortOptions = orderByIn.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            //get from U jsonmapping
            //var sourceTypeName = orderByIn.Split(new[] {' '});
            var typeName = typeof(TU).Name;
            if (!JsonPropertyMappings.ContainsKey(typeName))
                JsonPropertyMappings[typeName] = JsonHelper.GetJsonPropertyMapping(typeof(TU));

            var item =
                JsonPropertyMappings[typeName]
                    .Mapping.FirstOrDefault(
                        x =>
                            string.Compare(x.AttributeName, sortOptions[0],
                                StringComparison.OrdinalIgnoreCase) ==
                            0);
            var sourceColumnName = item != null ? item.PropertyName : sortOptions[0];

            var destPropertyName = "";
            // get U->T mapping
            var mapping = mapper.ConfigurationProvider.FindTypeMapFor<TU, T>();
            if (mapping != null)
            {
                var propertyMaps = mapping.GetPropertyMaps();
                if (propertyMaps != null)
                {
                    var sourceMap = propertyMaps.FirstOrDefault(
                        x =>
                            string.Compare(x.SourceMember.Name, sourceColumnName,
                                StringComparison.OrdinalIgnoreCase) == 0);

                    if (sourceMap != null)
                        destPropertyName = sourceMap.DestinationProperty.Name;
                }
            }

            if (string.IsNullOrEmpty(destPropertyName))
            {
                var property = typeof(T)
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(x => x.CanRead && x.CanWrite && x.Name == sourceColumnName);
                if (property == null)
                    return orderBy;

                destPropertyName = property.Name;
            }

            var ascDesc = "";
            if (sortOptions.Length > 1)
                ascDesc = sortOptions[1];
            return $"{destPropertyName} {ascDesc}";
        }

        public static PagedResult<T> BuildPagedResult<T>(List<T> page, int totalCount,
            int pageSize) where T : class
        {
            return new PagedResult<T>
            {
                Items = page,
                TotalCount = totalCount,
                TotalPages = (int) Math.Ceiling((double) totalCount / (pageSize <= 0 ? 10 : pageSize))
            };
        }
    }
}