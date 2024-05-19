using AutoMapper;
using WarehouseWebApi.Models.Db.Entity;

namespace WarehouseWebApi.Repository
{
    public class DbRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly IMapper _mapper;

        public DbRepository(WarehouseDbContext context)
        {
            _context = context;
            _mapper = Models.Business.RepositoryMapperConfig.InitializeAutomapper();
        }

        internal async Task AddPallet(Models.Business.Pallet palletBusiness, CancellationToken cancellationToken)
        {
            var palletEntity = _mapper.Map<PalletEntity>(palletBusiness);

            _context.Pallets.Add(palletEntity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        internal async Task<Models.Business.Pallet[]> GetPallets(CancellationToken token)
        {
            var pallets =
                await _context.GetAllAsync<PalletEntity, Models.Business.Pallet>(
                    pallet => true,
                    _mapper.ConfigurationProvider,
                    token);

            pallets.ToList().ForEach(p => p.Boxes.RemoveAll(b => b.OwnerId is not null));

            return pallets;
        }

        internal async Task<Models.Business.Pallet> GetPallet(string barcode, CancellationToken token)
        {
            var pallet =
                await _context.GetFirstOrDefaultAsync<PalletEntity, Models.Business.Pallet>(
                    pallet => pallet.Barcode == barcode,
                    _mapper.ConfigurationProvider,
                    token);

            pallet.Boxes.RemoveAll(b => b.OwnerId is not null);

            return pallet;
        }

        internal async Task RemoveBoxes(string[] barcodes, CancellationToken token)
        {
            foreach (var barcode in barcodes)
            {
                await _context.Delete<BoxEntity>(
                    box => box.Barcode == barcode,
                    token);
            }
        }
    }
}
