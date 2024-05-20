using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

            try
            {
                await _repository.AddPallet(palletBusiness, cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                if (exception?.InnerException is Npgsql.PostgresException innerException && innerException.SqlState == "23505")
                {
                    throw new InvalidDataException("Operation failed due to existing in warehouse barcode.");
                }
            }
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
            var availableBarcodes = await _repository.GetBoxesBarcodes(token);

            if (availableBarcodes.Intersect(barcodes).Count() != barcodes.Length)
            {
                throw new InvalidDataException("Operation denied due to unexisting in warehouse barcode.");
            }

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
                    }
                }
            }

            return modelState.ErrorCount > 0;
        }

        internal static bool IsInputInvalid(Models.View.Pallet pallet, ModelStateDictionary modelState)
        {
            var barcodes = CollectBarcodes(pallet.Boxes).ToArray();

            if ( barcodes.Distinct().Count() < barcodes.Length)
            {
                modelState.AddModelError($"pallet: {pallet.Barcode}", $"contains duplicate barcodes.");
            }

            HasInvalidBarcodes(barcodes, modelState);

            return modelState.ErrorCount > 0;
        }

        private static IEnumerable<string> CollectBarcodes(IEnumerable<Models.View.Box> boxes)
        {
            IEnumerable<string> barcodes = Enumerable.Empty<string>();

            boxes ??= Enumerable.Empty<Models.View.Box>();

            foreach (var box in boxes)
            {
                barcodes = barcodes.Append(box.Barcode);

                var subCodes = CollectBarcodes(box.Boxes);

                barcodes = barcodes.Concat(subCodes);
            }
            return barcodes;
        }
    }
}
