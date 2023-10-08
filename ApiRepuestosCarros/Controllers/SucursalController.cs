using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepuestosCarros.Controllers
{
    [ApiController]
    [Route("api/sucursal")]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalRepository _sucursalRepo;

        public SucursalController(ISucursalRepository sucursalRepo)
        {
            _sucursalRepo = sucursalRepo;
        }

        [HttpGet]
        [Route("listaSucursal")]
        public async Task<ActionResult> listaSucursal()
        {
            var sucursal = await _sucursalRepo.GetSucursales();
            if (sucursal is null)
            {
                return BadRequest("No existe ninguna sucursal creada.");
            }
            return Ok(sucursal);
        }

        [HttpGet]
        [Route("sucursalId")]
        public async Task<IActionResult> GetSucursal(int id)
        {
            var sucursal = await _sucursalRepo.GetSucursal(id);
            if (sucursal is null)
            {
                return NotFound();
            }
            return Ok(sucursal);
        }

        [HttpPost]
        [Route("crearSucursal")]
        public async Task<IActionResult> CrearSucursal([FromBody] Sucursal sucursal)
        {
            var result = _sucursalRepo.CreateSucursal(sucursal);

            if (result is null)
            {
                return BadRequest("Error al guardar la sucursal");
            }
            return Ok("Sucursal guardada exitosamente");
        }

        [HttpPut]
        [Route("actualizaSucursal")]
        public async Task<IActionResult> ActualizaSucursal(int id, [FromBody] Sucursal sucursal)
        {
            var result = await _sucursalRepo.GetSucursal(id);
            if (result is null)
            {
                return BadRequest("Sucursal no existe.");
            }
            await _sucursalRepo.UpdateSucursal(id, sucursal);

            return Ok("Sucursal actualizada exitosamente");
        }

        [HttpDelete]
        [Route("eliminarSucursal")]
        public async Task<IActionResult> EliminaSucursal(int id)
        {
            var result = await _sucursalRepo.GetSucursal(id);
            if (result is null)
            {
                return BadRequest("Sucursal no existe.");
            }

            await _sucursalRepo.DeleteSucursal(id);

            return Ok("Sucursal eliminada exitosamente");
        }
    }
}
