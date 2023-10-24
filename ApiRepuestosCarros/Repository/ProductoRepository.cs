using ApiRepuestosCarros.Context;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ApiRepuestosCarros.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DapperContext _context;

        public ProductoRepository(DapperContext context) => _context = context;

        public async Task CreateProducto(Producto producto)
        {
            var sql = "INSERT INTO producto(cod_producto,cod_barra,nombre,descripcion,precio,id_categoria,id_sucursal,image) VALUES " +
                     "(@cod_producto,@cod_barra,@nombre,@descripcion,@precio,@id_categoria,@id_sucursal,@image);";

            var parametros = new DynamicParameters();
            parametros.Add("cod_producto", producto.cod_producto, DbType.String);
            parametros.Add("cod_barra", producto.cod_barra, DbType.String);
            parametros.Add("nombre", producto.nombre, DbType.String);
            parametros.Add("descripcion", producto.descripcion, DbType.String);
            parametros.Add("precio", producto.precio, DbType.Decimal);
            parametros.Add("id_categoria", producto.id_categoria, DbType.Int32);
            parametros.Add("id_sucursal", producto.id_sucursal, DbType.Int32);
            parametros.Add("image", producto.image, DbType.Byte);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

        public async Task CreateProductoImagen(IFormFile file, string cod_prod, string cod_barr, string nom, string desc, decimal prec, int id_cat, int id_suc)
        {
            var sql = "INSERT INTO producto(cod_producto,cod_barra,nombre,descripcion,precio,id_categoria,id_sucursal,image) VALUES (@cod_producto,@cod_barra,@nombre,@descripcion,@precio,@id_categoria,@id_sucursal,@image);";
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var producto = new Producto
            {
                cod_producto = cod_prod,
                cod_barra = cod_barr,
                nombre = nom,
                descripcion = desc,
                precio = prec,
                id_categoria = id_cat,
                id_sucursal = id_suc,
                image = memoryStream.ToArray(),
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, producto);
            }
        }

        public async Task DeleteProducto(int id_producto)
        {
            var sql = "DELETE FROM producto WHERE id_producto = @id_producto;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { id_producto });
            }
        }

        public async Task<Producto> GetImagenProducto(int id_producto)
        {
            var sql = "SELECT image FROM producto WHERE id_producto = @id_producto;";

            using (var connection = _context.CreateConnection())
            {
                var producto = await connection.QuerySingleOrDefaultAsync<Producto>(sql, new { id_producto });
                return producto; 
            }

        }

        public async Task<Producto> GetProducto(int id_producto)
        {
            var sql = "SELECT id_producto,cod_producto,cod_barra,nombre,descripcion,precio,image FROM producto WHERE id_producto = @id_producto;";

            using (var connection = _context.CreateConnection())
            {
                var producto = await connection.QuerySingleOrDefaultAsync<Producto>(sql, new { id_producto });
                return producto;
            }
        }

       

        public async Task<IEnumerable<Producto>> GetProductos()
        {
            var sql = "SELECT * FROM producto";

            using (var connection = _context.CreateConnection())
            {
                var producto = await connection.QueryAsync<Producto>(sql);
                return producto.ToList();
            }
        }

        public Producto GetProductoS(int idProducto)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();

                string sql = @"
                SELECT 
                    p.*, 
                    c.*,
                    s.*,
                    u.*
                FROM producto p
                INNER JOIN categoria c ON p.id_categoria = c.id_categoria
                INNER JOIN sucursal s ON p.id_sucursal = s.id_sucursal
                INNER JOIN ubicacion u ON s.id_ubicacion = u.id_ubicacion
                WHERE p.id_producto = @IdProducto";

                var producto = connection.Query<Producto, Categoria, Sucursal,Ubicacion, Producto>(
                    sql,
                    (prod, cat, suc, ubi) =>
                    {
                        prod.Categoria = cat;
                        prod.Sucursal = suc;
                        suc.Ubicacion = ubi;
                        return prod;
                    },
                    new { IdProducto = idProducto },
                    splitOn: "id_categoria,id_sucursal,id_ubicacion"
                ).AsList().FirstOrDefault();

                return producto;
            }
        }

        public async Task<IEnumerable<Producto>> GetProductosSucursal(int id_sucursal)
        {
            var sql = "SELECT id_producto,cod_producto,cod_barra,nombre,descripcion,precio,id_sucursal,image FROM producto WHERE id_sucursal = @id_sucursal;";

            using (var connection = _context.CreateConnection())
            {
                var producto = await connection.QueryAsync<Producto>(sql, new { id_sucursal });
                return producto.ToList();
            }
        }

        public async Task UpdateProducto(int id_producto, Producto producto)
        {
            var sql = "UPDATE producto " +
                        "SET cod_producto = @cod_producto,cod_barra = @cod_barra,  nombre = @nombre, descripcion = @descripcion, precio = @precio, id_sucursal = @id_sucursal, id_categoria = @id_categoria " +
                        "WHERE id_producto = @id_producto;";

            var parametros = new DynamicParameters();
            parametros.Add("id_producto", id_producto, DbType.Int32);
            parametros.Add("cod_producto", producto.cod_producto, DbType.String);
            parametros.Add("cod_barra", producto.cod_barra, DbType.String);
            parametros.Add("nombre", producto.nombre, DbType.String);
            parametros.Add("descripcion", producto.descripcion, DbType.String);
            parametros.Add("precio", producto.precio, DbType.Decimal);
            parametros.Add("id_categoria", producto.id_categoria, DbType.Int32);
            parametros.Add("id_sucursal", producto.id_sucursal, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

       
    }
}
