using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Contracts
{
    public interface IUsuariosRepository
    {
        public Task<Usuarios> GetUsuario(Login login);
        public Task RegistroUsuario( string usuario, string password, string email);
    }
}
