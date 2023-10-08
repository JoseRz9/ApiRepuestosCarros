using ApiRepuestosCarros.Context;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using System.Data;

namespace ApiRepuestosCarros.Repository
{
    public class SucursalRepository :ISucursalRepository
    {
        private readonly DapperContext _context;

        public SucursalRepository(DapperContext context) => _context = context;

        public async Task CreateSucursal(Sucursal sucursal)
        {
            var sql = "INSERT INTO sucursal(cod_sucursal,nombre,id_ubicacion) VALUES (@cod_sucursal,@nombre,@id_ubicacion);";

            var parametros = new DynamicParameters();
            parametros.Add("cod_sucursal", sucursal.cod_sucursal, DbType.String);
            parametros.Add("nombre", sucursal.nombre, DbType.String);
            parametros.Add("id_ubicacion", sucursal.id_ubicacion, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

        public async Task DeleteSucursal(int id_sucursal)
        {
            var sql = "DELETE FROM sucursal WHERE id_sucursal = @id_sucursal;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { id_sucursal });
            }
        }

        public async Task<Sucursal> GetSucursal(int id_sucursal)
        {
            var sql = "select id_sucursal,cod_sucursal,nombre from sucursal WHERE id_sucursal = @id_sucursal;";

            using (var connection = _context.CreateConnection())
            {
                var sucursal = await connection.QuerySingleOrDefaultAsync<Sucursal>(sql, new { id_sucursal });
                return sucursal;
            }
        }

        public async Task<IEnumerable<Sucursal>> GetSucursales()
        {
            var sql = "SELECT id_sucursal,cod_sucursal,nombre,id_ubicacion FROM sucursal;";

            using (var connection = _context.CreateConnection())
            {
                var sucursal = await connection.QueryAsync<Sucursal>(sql);
                return sucursal;
            }
        }

        public async Task UpdateSucursal(int id_sucursal, Sucursal sucursal)
        {
            var sql = "UPDATE sucursal SET cod_sucursal = @cod_sucursal, nombre = @nombre, id_ubicacion = @id_ubicacion WHERE id_sucursal = @id_sucursal;";

            var parametros = new DynamicParameters();
            parametros.Add("id_sucursal", id_sucursal, DbType.Int32);
            parametros.Add("cod_sucursal", sucursal.cod_sucursal, DbType.String);
            parametros.Add("nombre", sucursal.nombre, DbType.String);
            parametros.Add("id_ubicacion", sucursal.id_ubicacion, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }
    }
}
