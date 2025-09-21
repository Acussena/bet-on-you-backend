using backend.src.Models;
using backend.src.Requests;
using backend.src.Services;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace backend.src.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _service = new();

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] UsuarioRequest usuarioRequest)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = usuarioRequest.Nome,
                    Email = usuarioRequest.Email,
                    Senha = usuarioRequest.Senha
                };

                var cadastrado = _service.Cadastrar(usuario);

                if (!cadastrado)
                {
                    return BadRequest(new { message = "Email já cadastrado." });
                }

                return Created("", new
                {
                    message = "Usuário cadastrado com sucesso!",
                    usuario = new
                    {
                        usuario.Nome,
                        usuario.Email
                    }
                });
            }
            catch (OracleException ex) when (ex.Number == 1)
            {
                return BadRequest(new { message = "Email já cadastrado." });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioRequest usuarioRequest)
        {
            return _service.Login(usuarioRequest.Email, usuarioRequest.Senha)
                ? Ok(new { message = "Login realizado!" })
                : Unauthorized(new { message = "Usuário ou senha inválidos." });
        }
    }
}
