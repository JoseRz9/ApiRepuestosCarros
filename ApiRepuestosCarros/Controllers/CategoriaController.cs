using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiRepuestosCarros.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/categoria")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepo;

        public CategoriaController(ICategoriaRepository categoriaRepo) 
        {
            _categoriaRepo = categoriaRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("listaCategoria")]
        public async Task<ActionResult> listaCategoria()
        {
           var categoria = await _categoriaRepo.GetCategorias();
            return Ok(categoria);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("categoriaId")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var categoria = await _categoriaRepo.GetCategoria(id);
            if (categoria is null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }


        [HttpPost]
        [Authorize]
        [Route("crearCategoria")]
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            var result =  _categoriaRepo.CreateCategoria(categoria);

            if (result is  null)
            {
                return BadRequest("Error al guardar categoria");
            }
            return Ok("Categegoria guardada exitosamente");
        }

        [HttpPut]
        [Authorize]
        [Route("actualizaCategoria")]
        public async Task<IActionResult> ActualizaCategoria(int id, [FromBody] Categoria categoria)
        {
            var result = await _categoriaRepo.GetCategoria(id);
            if (result is null)
            {
               return BadRequest("Categoria no existe.");
            }
            await _categoriaRepo.UpdateCategoria(id, categoria);

            return Ok("Categegoria actualizada exitosamente");
        }

        [HttpDelete]
        [Authorize]
        [Route("eliminarCategoria")]
        public async Task<IActionResult> EliminaCategoria(int id)
        {
            var result = await _categoriaRepo.GetCategoria(id);
            if (result is null)
            {
                return BadRequest("Categoria no existe.");
            }

            await _categoriaRepo.DeleteCategoria(id);

            return Ok("Categoria eliminada exitosamente");
        }

        //[HttpGet]
        //[Route("categoriaProductos")]
        //public async Task<IActionResult> ListaProductosCategoria(int id)
        //{
        //    var result = await _categoriaRepo.GetCategoriaProducto(id);
        //    if (result is null)
        //    {
        //        return BadRequest("Categoria no existe.");
        //    }
        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("listaProductoResultado")]
        //public async Task<IActionResult> ListaProductoResultado()
        //{
        //    var categoria = await _categoriaRepo.GetAllCategoriasProductos();

        //    return Ok(categoria);
        //}
    }
}
