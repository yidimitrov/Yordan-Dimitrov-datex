using AutoMapper;
using WarehouseWebApi.Models.Db.Entity;

namespace WarehouseWebApi.Models.Business
{
    internal static class RepositoryMapperConfig
    {
        internal static MapperConfiguration Configuration = new(cfg =>
        {
            cfg.CreateMap<Pallet, PalletEntity>();
            cfg.CreateMap<Box, BoxEntity>();

            cfg.CreateMap<PalletEntity, Pallet>();
            cfg.CreateMap<BoxEntity, Box>();
        });

        internal static Mapper InitializeAutomapper()
        {
            var mapper = new Mapper(Configuration);
            return mapper;
        }
    }
}
