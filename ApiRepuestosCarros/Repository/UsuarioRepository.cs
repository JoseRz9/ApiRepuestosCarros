using ApiRepuestosCarros.Context;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.Models;
using Dapper;
using System.Data;

namespace ApiRepuestosCarros.Repository
{
    public class UsuarioRepository: IUsuariosRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context) => _context = context;

        public async Task<Usuarios> GetUsuario(Login login)
        {
            using (var connection = _context.CreateConnection())
            {
                
                var parametro = new DynamicParameters();
                parametro.Add("@usuarioApi", login.usuario, DbType.String, ParameterDirection.Input, 500);
                parametro.Add("@passwordApi", login.password, DbType.String, ParameterDirection.Input, 500);

                var usu = await connection.QuerySingleOrDefaultAsync<Usuarios>("UsuariosAPIObtner", parametro, commandType: CommandType.StoredProcedure);
                return usu;
            }
        }

        //public async Task RegistroUsuario(string usuario, string password, string email)
        //{
        //    using (var connection = _context.CreateConnection())
        //    {

        //        var parametro = new DynamicParameters();
        //        parametro.Add("@usuarioApi", usuario, DbType.String, ParameterDirection.Input, 500);
        //        parametro.Add("@passwordApi", password, DbType.String, ParameterDirection.Input, 500);
        //        parametro.Add("@emailUsuario", email, DbType.String, ParameterDirection.Input, 500);

        //        var usu = await connection.QuerySingleOrDefaultAsync<Usuarios>("UsuariosAPIAlta", parametro, commandType: CommandType.StoredProcedure);
        //        return usu;
        //    }
        //}


        public async Task RegistroUsuario(string usuario, string password, string email)
        {
            var sql = "UsuariosAPIAlta";
            var parametro = new DynamicParameters();
            parametro.Add("@usuarioApi", usuario, DbType.String, ParameterDirection.Input, 500);
            parametro.Add("@passwordApi", password, DbType.String, ParameterDirection.Input, 500);
            parametro.Add("@emailUsuario", email, DbType.String, ParameterDirection.Input, 500);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql, parametro, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
