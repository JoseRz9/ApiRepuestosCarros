using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ApiRepuestosCarros.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/producto")]
    
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepo;

        public ProductoController(IProductoRepository productoRepo)
        {
            _productoRepo = productoRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("listaProducto")]
        public async Task<ActionResult<Producto>> listaProducto()
        {

            var producto = await _productoRepo.GetProductos();
            if (producto is null)
            {
                return BadRequest("No existe ningun producto creado.");
            }
            return Ok(producto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("listaProductoSucursal")]
        public async Task<ActionResult<Producto>> listaProductoSucursal(int id)
        {

            var producto = await _productoRepo.GetProductosSucursal(id);
            if (producto is null)
            {
                return BadRequest("No existe ningun en la sucursal seleccionada.");
            }
            return Ok(producto);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("productoId")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _productoRepo.GetProducto(id);
            if (producto is null)
            {
                return NotFound();
            }
            return Ok(producto);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("productoIdImage")]
        public async Task<IActionResult> GetProductoImage(int id)
        {
            var producto = await _productoRepo.GetImagenProducto(id);
            if (producto is null)
            {
                return NotFound();
            }
            //imagen
            return File(producto.image);
        }

        private IActionResult File(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return NotFound(); // Retorna un 404 si no hay datos de imagen
            }

            return new FileContentResult(imageData, "image/jpeg");
        }


        [HttpPost]
        [Authorize]
        [Route("crearProducto")]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            var result = _productoRepo.CreateProducto(producto);

            if (result is null)
            {
                return BadRequest("Error al guardar el producto");
            }
            return Ok("Producto guardada exitosamente");
        }

        [HttpPost]
        [Authorize]
        [Route("crearProductoImage")]
        public async Task<IActionResult> CrearProductoimage(IFormFile file, string cod_prod, string cod_barr, string nom, string desc, decimal prec, int id_cat, int id_suc)
        {
            var result = _productoRepo.CreateProductoImagen(file,cod_prod,cod_barr,nom,  desc,  prec,  id_cat,  id_suc);

            if (result is null)
            {
                return BadRequest("Error al guardar el producto");
            }
            return Ok("Producto guardada exitosamente");
        }


        [HttpPut]
        [Authorize]
        [Route("actualizaProducto")]
        public async Task<IActionResult> ActualizaProducto(int id, [FromBody] Producto producto)
        {
            var result = await _productoRepo.GetProducto(id);
            if (result is null)
            {
                return BadRequest("Producto no existe.");
            }
            await _productoRepo.UpdateProducto(id, producto);

            return Ok("Producto actualizad exitosamente");
        }

        [HttpDelete]
        [Authorize]
        [Route("eliminarUbicacion")]
        public async Task<IActionResult> EliminaProducto(int id)
        {
            var result = await _productoRepo.GetProducto(id);
            if (result is null)
            {
                return BadRequest("Producto no existe.");
            }

            await _productoRepo.DeleteProducto(id);

            return Ok("Producto eliminado exitosamente");
        }

        //[HttpGet]
        //[Route("listaProducto")]
        //public async Task<ActionResult<List<Producto>>> listaProducto()
        //{
        //const string sql_consul = @"select p.*,
        //                                c.id_categoria,c.nombre as nom_categoria,
        //                                s.id_sucursal,s.nombre as nom_sucursal 
        //                            from producto p 
        //                            inner join categoria c on c.id_categoria = p.id_categoria
        //                            inner join sucursal s on s.id_sucursal = p.id_sucursal;";

        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    var producto = await connection.Query<Producto>(sql_consul, "");
        //    return Ok(producto);
        //}


        //[HttpGet]
        //[Route("listaProducto")]
        //public async Task<ActionResult<List<Producto>>> listaProducto()
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    try
        //    {
        //        const string sql = @"select p.*,
        //                                c.id_categoria,c.cod_categoria, c.nombre, 
        //                                s.id_sucursal,s.cod_sucursal,s.nombre
        //                            from producto p 
        //                            inner join categoria c on p.id_categoria = c.id_categoria
        //                            inner join sucursal s on s.id_sucursal = p.id_sucursal;";


        //        var productos = await connection.QueryAsync<Producto, Categoria, Sucursal, Producto>(
        //            sql,
        //            (producto, categoria, sucursal) =>
        //            {
        //                producto.Categoria = categoria;
        //                producto.Sucursal = sucursal;
        //                return producto;
        //            },
        //            splitOn: "id_categoria,id_sucursal"
        //        );

        //        return productos.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //[HttpGet]
        //[Route("listaProducto2")]
        //public async Task<ActionResult<List<Producto>>> listaProductog()
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    try
        //    {
        //        const string sql = @"select * from producto;";


        //        var productos = await connection.QueryAsync<Producto>(sql);

        //        return productos.ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}


        //[HttpPost]
        //[Route("guardaProducto")]
        //public async Task<IActionResult> guardaProducto([FromBody] Producto producto)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    try
        //    {
        //        string sQuery = @"INSERT INTO producto (cod_producto, cod_barra, nombre, descripcion, precio, url_imagen, imagen, id_categoria, id_sucursal)
        //                      VALUES(@CodProducto, @CodBarra, @Nombre, @Descripcion, @Precio, @UrlImagen, @Imagen, @IdCategoria, @IdSucursal)";
        //        var result = await connection.ExecuteAsync(sQuery, producto);
        //        if (result > 0)
        //        {
        //            return Ok("Producto agregado exitosamente");
        //        }
        //        return BadRequest("Error al agregar el producto");
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
