using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using WarehouseWebApi.Mappers;
using WarehouseWebApi.Repository;
using static WarehouseWebApi.Models.Business.ModelsExtensions;

namespace WarehouseWebApi.Services
{
    public class WarehouseService
    {
        private readonly DbRepository _repository;
        private readonly IMapper _mapper;

        public WarehouseService(DbRepository repository)
        {
            _repository = repository;
            _mapper = ServiceMapperConfig.InitializeAutomapper();
        }
        
        internal async Task AddPallet(Models.View.Pallet pallet, CancellationToken cancellationToken)
        {
            var palletBusiness = _mapper.Map<Models.Business.Pallet>(pallet);

            BuidDataIntegrity(palletBusiness.Boxes, null, palletBusiness);

            await _repository.AddPallet(palletBusiness, cancellationToken);
        }

        internal async Task<Models.View.Pallet[]> GetPallets(CancellationToken token)
        {
            var pallets = await _repository.GetPallets(token);

            var palletsView = _mapper.Map<Models.View.Pallet[]>(pallets);

            return palletsView;
        }

        internal async Task<Models.View.Pallet> GetPallet(string barcode, CancellationToken token)
        {
            var pallet = await _repository.GetPallet(barcode, token);

            var palletView = _mapper.Map<Models.View.Pallet>(pallet);

            return palletView;
        }

        internal async Task RemoveBoxes(string[] barcodes, CancellationToken token)
        {
            await _repository.RemoveBoxes(barcodes, token);
        }

        internal static bool HasInvalidBarcodes(string[] barcodes, ModelStateDictionary modelState)
        {
            string pattern = @"^[0-9a-zA-Z]+$";

            foreach (var barcode in barcodes)
            {
                if (!string.IsNullOrEmpty(barcode))
                { 
                    Match m = Regex.Match(barcode, pattern, RegexOptions.IgnoreCase);

                    if (!m.Success)
                    {
                        modelState.AddModelError("barcode", $"'{barcode}' is not alpha numeric");
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
