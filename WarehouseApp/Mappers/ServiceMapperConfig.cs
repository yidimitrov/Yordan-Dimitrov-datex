using AutoMapper;

namespace WarehouseWebApi.Mappers
{
    internal static class ServiceMapperConfig
    {
        internal static MapperConfiguration Configuration = new (cfg =>
        {
            cfg.CreateMap<Models.View.Pallet, Models.Business.Pallet>().ReverseMap();
            cfg.CreateMap<Models.View.Box, Models.Business.Box>().ReverseMap();
        });

        internal static Mapper InitializeAutomapper()
        {
            var mapper = new Mapper(Configuration);
            return mapper;
        }
    }
}
