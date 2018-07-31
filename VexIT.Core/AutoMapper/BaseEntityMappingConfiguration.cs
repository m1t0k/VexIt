using System;
using AutoMapper;
using VexIT.DataAccess.Model;
using VexIT.DataContracts.V1.Data;

namespace VexIT.Core.AutoMapper
{
    public class BaseEntityMappingConfiguration
    {
        public static void InitMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<BaseDto, BaseDto>()
                .ForMember(x => x.Id, op => op.MapFrom(t => Guid.Empty))
                .ForAllOtherMembers(op => op.Ignore());

            cfg.CreateMap<EntityBase, EntityBase>()
                .ForMember(x => x.Id, op => op.MapFrom(t => Guid.Empty))
                .ForAllOtherMembers(op => op.Ignore());

            cfg.CreateMap<EntityBase, BaseDto>();

            cfg.CreateMap<BaseDto, EntityBase>()
                .ForMember(p => p.Id, op => op.MapFrom(x => x.Id));
        }
    }
}