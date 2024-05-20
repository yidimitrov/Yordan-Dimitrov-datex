using AutoMapper;
using WarehouseWebApi.Models.Business;
using WarehouseWebApi.Models.Db.Entity;

namespace WarehouseWebApi.Mappers
{
    internal static class RepositoryMapperConfig
    {
        internal static MapperConfiguration Configuration = new(cfg =>
        {
            cfg.CreateMap<Pallet, PalletEntity>();
            cfg.CreateMap<Box, BoxEntity>();

            cfg.CreateMap<PalletEntity, Pallet>();
            cfg.CreateMap<BoxEntity, Box>();

            cfg.CreateMap<BoxEntity, BarcodesDto>()
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode));
        });

        internal static Mapper InitializeAutomapper()
        {
            var mapper = new Mapper(Configuration);
            return mapper;
        }
    }
}
