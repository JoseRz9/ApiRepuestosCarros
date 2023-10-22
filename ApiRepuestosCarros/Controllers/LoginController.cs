using ApiRepuestosCarros.Clases;
using ApiRepuestosCarros.Contracts;
using ApiRepuestosCarros.DTO;
using ApiRepuestosCarros.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRepuestosCarros.Controllers
{

    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {

        private readonly IUsuariosRepository _usuarioRepo;
        private readonly IConfiguration configuration;

        public LoginController(IUsuariosRepository usuarioRepo, IConfiguration configuration)
        {
            this._usuarioRepo = usuarioRepo;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuariosDTO>> Login(Login usuarioLogin)
        {
            Usuarios usuario = null;
            usuario = await AutenticarUsuarioAsync(usuarioLogin);

            if (usuario == null)
            {
                throw new Exception("Credenciales No validas");
            }
            else
            {
                usuario = GenerarTokenJWT(usuario);
            }



            return usuario.convertDTO();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registroUsuario")]
        public async Task<IActionResult> RegistroUsuario(string usuario, string password, string email)
        {
            var result = _usuarioRepo.RegistroUsuario(usuario,password,email);

            if (result is null)
            {
                return BadRequest("Error al registrarse");
            }
            return Ok("Registro Existoso");
        }


        [HttpPost]
        private async Task<Usuarios> AutenticarUsuarioAsync(Login usuarioLogin)
        {
            Usuarios usuario = await _usuarioRepo.GetUsuario(usuarioLogin);
            return usuario;
        }

        private Usuarios GenerarTokenJWT(Usuarios usuarioInfo)
        {
            //Cabecera
            var _symmetricSecuritykey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecuritykey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            //Claims
            var _Claim = new[]
            {
                new Claim("usuario", usuarioInfo.usuario),
                new Claim("email", usuarioInfo.email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.email)

            };

            //PayLoad
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    claims: _Claim,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(30)
                );

            //Token
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            usuarioInfo.token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;
        }   

    }
}
