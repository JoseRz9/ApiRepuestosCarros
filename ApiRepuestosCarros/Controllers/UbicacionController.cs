using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRepuestosCarros.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ubicacion")]
    
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionRepository _ubicacionRepo;

        public UbicacionController(IUbicacionRepository ubicacionRepo)
        {
            _ubicacionRepo = ubicacionRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("listaUbicacion")]
        public async Task<ActionResult> listaUbicacion()
        {
            var ubicacion = await _ubicacionRepo.GetUbicaciones();
            if (ubicacion is null)
            {
                return BadRequest("No existe ninguna ubicacion creada.");
            }
            return Ok(ubicacion);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("ubicacionId")]
        public async Task<IActionResult> GetUbicacion(int id)
        {
            var ubicacion = await _ubicacionRepo.GetUbicacion(id);
            if (ubicacion is null)
            {
                return NotFound();
            }
            return Ok(ubicacion);
        }

        [HttpPost]
        [Authorize]
        [Route("crearUbicacion")]
        public async Task<IActionResult> CrearUbicacion([FromBody] Ubicacion ubicacion)
        {
            var result =  _ubicacionRepo.CreateUbicacion(ubicacion);

            if (result is null)
            {
                return BadRequest("Error al guardar la ubicacion");
            }
            return Ok("Ubicacion guardada exitosamente");
        }

        [HttpPut]
        [Authorize]
        [Route("actualizaUbicacion")]
        public async Task<IActionResult> ActualizaUbicacion(int id, [FromBody] Ubicacion ubicacion)
        {
            var result = await _ubicacionRepo.GetUbicacion(id);
            if (result is null)
            {
                return BadRequest("Ubicacion no existe.");
            }
            await _ubicacionRepo.UpdateUbicacion(id, ubicacion);

            return Ok("Ubicacion actualizada exitosamente");
        }

        [HttpDelete]
        [Authorize]
        [Route("eliminarUbicacion")]
        public async Task<IActionResult> EliminaUbicacion(int id)
        {
            var result = await _ubicacionRepo.GetUbicacion(id);
            if (result is null)
            {
                return BadRequest("Sucursal no existe.");
            }

            await _ubicacionRepo.DeleteUbicacion(id);

            return Ok("Sucursal eliminada exitosamente");
        }
    }
}
