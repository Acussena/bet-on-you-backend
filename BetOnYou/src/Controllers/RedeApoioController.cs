using backend.src.Models;
using backend.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.Controllers
{
    [ApiController]
    [Route("api/redeApoio")]
    public class RedeApoioController : ControllerBase
    {
        private readonly RedeApoioService _service = new();

        [HttpGet]
        public ActionResult<List<RedeApoio>> GetAll()
        {
            try
            {
                var redes = _service.ListarRedes();
                return Ok(redes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao carregar redes de apoio", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<RedeApoio> GetById(int id)
        {
            try
            {
                var rede = _service.BuscarPorId(id);
                if (rede == null)
                    return NotFound(new { Message = "Rede de apoio não encontrada" });

                return Ok(rede);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao buscar rede de apoio", Details = ex.Message });
            }
        }
    }
}
