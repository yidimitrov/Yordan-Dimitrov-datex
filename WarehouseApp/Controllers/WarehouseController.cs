using Microsoft.AspNetCore.Mvc;
using WarehouseWebApi.Models.View;
using WarehouseWebApi.Services;

namespace WarehouseWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;

        public WarehouseController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost(Name = "AddPallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddPallet([FromBody] Pallet pallet, CancellationToken cancellationToken)
        {
            await _warehouseService.AddPallet(pallet, cancellationToken);
            return Ok();
        }

        [HttpGet(Name = "GetPallets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pallet[]>> Get(CancellationToken cancellationToken)
        {
            return Ok(await _warehouseService.GetPallets(cancellationToken));
        }

        [HttpGet("{barcode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pallet[]>> Get([FromRoute] string barcode, CancellationToken cancellationToken)
        {
            if (WarehouseService.HasInvalidBarcodes(new[] { barcode }, ModelState))
            {
                return BadRequest(ModelState);
            }

            var pallet = await _warehouseService.GetPallet(barcode, cancellationToken);

            return pallet is null ? NotFound() : Ok(pallet);
        }

        [HttpPut(Name = "RemoveBoxes")]
        public async Task<ActionResult<Pallet[]>> RemoveBoxes([FromBody] string[] barcodes, CancellationToken cancellationToken)
        {
            if (WarehouseService.HasInvalidBarcodes(barcodes, ModelState))
            {
                return BadRequest(ModelState);
            }

            await _warehouseService.RemoveBoxes(barcodes, cancellationToken);
            return Ok();
        }
    }
}