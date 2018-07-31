using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace VexIT.Core.AutoMapper
{
    public class AutoMapperConfiguration
    {
        private static readonly object SyncObject = new object();
        private static volatile IMapper _mapper;

        /// <summary>
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                if (_mapper != null) return _mapper;

                lock (SyncObject)
                {
                    if (_mapper != null) return _mapper;

                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddCollectionMappers();
                        cfg.AllowNullCollections = true;
                        cfg.AllowNullDestinationValues = true;
                        BaseEntityMappingConfiguration.InitMappings(cfg);
                        BusinessMappingConfiguration.InitMappings(cfg);
                    });

                    configuration.CompileMappings();
                    configuration.AssertConfigurationIsValid();

                    _mapper = configuration.CreateMapper();
                }

                return _mapper;
            }
        }
    }
}