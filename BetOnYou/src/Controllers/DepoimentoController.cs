using backend.src.Models;
using backend.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers
{
    [ApiController]
    [Route("api/depoimentos")]
    public class DepoimentosController : ControllerBase
    {
        private readonly DepoimentoService _service = new();

        [HttpGet] // GET /api/depoimentos
        public IActionResult GetAll()
        {
            var todos = _service.ListarTodos();
            return Ok(todos);
        }

        [HttpGet("{id:int}")] // GET /api/depoimentos/1
        public IActionResult GetById(int id)
        {
            var d = _service.ObterPorId(id);
            if (d == null)
                return NotFound(new { Message = "Depoimento não encontrado." });
            return Ok(d);
        }

        [HttpPost] // POST /api/depoimentos
        public IActionResult Post([FromBody] Depoimento d)
        {
            try
            {
                var id = _service.Criar(d);
                if (id <= 0)
                    return BadRequest(new { Message = "Erro ao criar depoimento." });

                return Ok(new { Message = "Depoimento criado com sucesso.", Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id:int}")] // PUT /api/depoimentos/1
        public IActionResult Put(int id, [FromBody] Depoimento d)
        {
            try
            {
                _service.Atualizar(id, d);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")] // DELETE /api/depoimentos/1
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
