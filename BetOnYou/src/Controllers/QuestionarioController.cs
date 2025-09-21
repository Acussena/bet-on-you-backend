using backend.src.Models;
using backend.src.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace backend.src.Controllers
{
    [ApiController]
    [Route("api/questionario")]
    public class QuestionarioController : ControllerBase
    {
        private readonly QuestionarioService _service = new();

        [HttpPost]
        public IActionResult Post([FromBody] Questionario q)
        {
            try
            {
                int idGerado = _service.RegistrarRespostas(q);
                if (idGerado <= 0)
                    return BadRequest(new { Message = "Erro ao cadastrar questionário." });

                return Ok(new { Message = "Questionário registrado com sucesso", Id = idGerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
