using System;
using System.Collections.Generic;
using AutoMapper;
using VexIT.DataAccess.Model;
using VexIT.DataContracts.V1.Business;
using VexIT.DataContracts.V1.Data;

namespace VexIT.Core.AutoMapper
{
    public class BusinessMappingConfiguration
    {
        public static void InitMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<EventDto, Event>()
                .IncludeBase<BaseDto, EntityBase>().ReverseMap();
        }
    }
}