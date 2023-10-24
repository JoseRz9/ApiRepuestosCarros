using ApiRepuestosCarros.DTO;
using ApiRepuestosCarros.Models;

namespace ApiRepuestosCarros.Clases
{
    public static class Utilidades
    {
        public static UsuariosDTO convertDTO(this Usuarios u)
        {
            if (u != null)
            {
                return new UsuariosDTO
                {
                    token = u.token,
                    usuario = u.usuario,
                    email = u.email
                };
            }

            return null;
        }
    }

    
}
