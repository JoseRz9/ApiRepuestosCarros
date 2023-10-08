using ApiRepuestosCarros.Context;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using System.Data;

namespace ApiRepuestosCarros.Repository
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly DapperContext _context;

        public UbicacionRepository(DapperContext context) => _context = context;

        public async Task CreateUbicacion(Ubicacion ubicacion)
        {
            var sql = "INSERT INTO ubicacion(cod_ubicacion,nombre,longitud,latitud) VALUES (@cod_ubicacion,@nombre,@longitud,@latitud);";

            var parametros = new DynamicParameters();
            parametros.Add("cod_ubicacion", ubicacion.cod_ubicacion, DbType.String);
            parametros.Add("nombre", ubicacion.nombre, DbType.String);
            parametros.Add("longitud", ubicacion.longitud, DbType.Decimal);
            parametros.Add("latitud", ubicacion.latitud, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }

        public async Task DeleteUbicacion(int id_ubicacion)
        {
            var sql = "DELETE FROM ubicacion WHERE id_ubicacion = @id_ubicacion;";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { id_ubicacion });
            }
        }

        public async Task<Ubicacion> GetUbicacion(int id_ubicacion)
        {
            var sql = "SELECT cod_ubicacion,nombre,longitud,latitud FROM ubicacion WHERE id_ubicacion = @id_ubicacion;";

            using (var connection = _context.CreateConnection())
            {
                var ubicacion = await connection.QuerySingleOrDefaultAsync<Ubicacion>(sql, new { id_ubicacion });
                return ubicacion;
            }
        }

        public async Task<IEnumerable<Ubicacion>> GetUbicaciones()
        {
            var sql = "SELECT id_ubicacion,cod_ubicacion,nombre,longitud,latitud FROM ubicacion;";

            using (var connection = _context.CreateConnection())
            {
                var ubicacion = await connection.QueryAsync<Ubicacion>(sql);
                return ubicacion.ToList();
            }
        }

        public async Task UpdateUbicacion(int id_ubicacion, Ubicacion ubicacion)
        {
            var sql = "UPDATE ubicacion SET cod_ubicacion = @cod_ubicacion, nombre = @nombre, longitud = @longitud, latitud = @latitud WHERE id_ubicacion = @id_ubicacion;";

            var parametros = new DynamicParameters();
            parametros.Add("id_ubicacion", id_ubicacion, DbType.Int32);
            parametros.Add("cod_ubicacion", ubicacion.cod_ubicacion, DbType.String);
            parametros.Add("nombre", ubicacion.nombre, DbType.String);
            parametros.Add("longitud", ubicacion.longitud, DbType.Decimal);
            parametros.Add("latitud", ubicacion.latitud, DbType.Decimal);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametros);
            }
        }
    }
}
