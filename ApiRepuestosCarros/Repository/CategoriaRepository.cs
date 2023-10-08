using ApiRepuestosCarros.Context;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using System.Data;

namespace ApiRepuestosCarros.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DapperContext _context;

        public CategoriaRepository(DapperContext context) => _context = context;

        //Crea una nueva categoria
        public async Task CreateCategoria(Categoria categoria)
        {
            var sql = "INSERT INTO categoria(cod_categoria,nombre) VALUES (@cod_categoria, @nombre);";

            var parametros = new DynamicParameters();
            parametros.Add("cod_categoria", categoria.cod_categoria, DbType.String);
            parametros.Add("nombre", categoria.nombre, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

        //Elimina una categoria
        public async Task DeleteCategoria(int id_categoria)
        {
            var sql = "DELETE FROM categoria WHERE id_categoria = @id_categoria;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { id_categoria });
            }
        }

        //Muestra una categoria en especifico
        public async Task<Categoria> GetCategoria(int id_categoria)
        {
            var sql = "SELECT * FROM categoria WHERE id_categoria = @id_categoria;";

            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QuerySingleOrDefaultAsync<Categoria>(sql, new { id_categoria });
                return categoria;
            }
        }

        //Muestra listado de categorias
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var sql = "SELECT * FROM categoria;";

            using (var connection = _context.CreateConnection())
            {
                var categoria = await connection.QueryAsync<Categoria>(sql);
                return categoria.ToList();
            }
        }

        //actualiza una categoria
        public async Task UpdateCategoria(int id_categoria, Categoria categoria)
        {
            var sql = "UPDATE categoria SET cod_categoria = @cod_categoria, nombre = @nombre WHERE id_categoria = @id_categoria;";

            var parametros = new DynamicParameters();
            parametros.Add("id_categoria", id_categoria, DbType.Int32);
            parametros.Add("cod_categoria", categoria.cod_categoria, DbType.String);
            parametros.Add("nombre", categoria.nombre, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

        //public async Task<List<Categoria>> GetAllCategoriasProductos()
        //{
        //    var sql = "SELECT * FROM categoria c INNER JOIN producto p ON c.id_categoria = p.id_categoria";

        //    using (var connection = _context.CreateConnection())
        //    {
        //        var categoriaDic = new Dictionary<int, Categoria>();

        //        var categoria = await connection.QueryAsync<Categoria, Producto, Categoria>(
        //            sql, (categoria,producto) =>
        //            {
        //                if (!categoriaDic.TryGetValue(categoria.id_categoria, out var currentCategoria))
        //                {
        //                    currentCategoria = categoria;
        //                    categoriaDic.Add(currentCategoria.id_categoria, currentCategoria);
        //                }

        //                currentCategoria.Producto.Add(producto);
        //                return currentCategoria;
        //            }
        //        );

        //        return categoria.Distinct().ToList();
        //    }
        //}



        //trae todo los productos relacionado a una categoria
        //public async Task<Categoria> GetCategoriaProducto(int id_categoria)
        //{
        //    var sql = "SELECT * FROM categoria WHERE id_categoria = @id_categoria;"+
        //                "SELECT * FROM producto WHERE id_categoria = @id_categoria";

        //    using (var connection = _context.CreateConnection())
        //    using (var mutiple = await connection.QueryMultipleAsync(sql, new {id_categoria}))
        //    {
        //        var categoria = await mutiple.ReadSingleOrDefaultAsync<Categoria>();
        //        if (categoria is null)
        //        {
        //            categoria.Producto = (await mutiple.ReadAsync<Producto>()).ToList();
        //        }
        //        return categoria;
        //    }

        //}


    }
}
