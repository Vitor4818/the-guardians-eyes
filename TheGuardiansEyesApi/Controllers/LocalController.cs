using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly LocalService _localService;

        public LocalController(LocalService localService)
        {
            _localService = localService;
        }

        // GET: api/local
        [HttpGet]
        public IActionResult Get()
        {
            var locais = _localService.ListarLocais();
            if (locais == null || locais.Count == 0)
                return NoContent();

            return Ok(locais);
        }

        // GET: api/local/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var local = _localService.ObterPorId(id);
            if (local == null)
                return NotFound();

            return Ok(local);
        }

        // GET: api/local/coordenadas?latitude=...&longitude=...
        [HttpGet("coordenadas")]
        public IActionResult GetPorCoordenadas([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var local = _localService.ObterPorCoordenadas(latitude, longitude);
            if (local == null)
                return NotFound();

            return Ok(local);
        }

        // POST: api/local
        [HttpPost]
        public IActionResult Post([FromBody] LocalModel local)
        {
            // Validações mínimas
            if (local == null || local.Latitude == 0 || local.Longitude == 0)
                return BadRequest("Latitude e Longitude devem ser fornecidas e válidas.");

            var criado = _localService.CadastrarLocal(local);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/local/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LocalModel local)
        {
            if (local == null || local.Id != id)
                return BadRequest("Dados inconsistentes.");

            var atualizado = _localService.AtualizarLocal(local);
            if (!atualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/local/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var removido = _localService.RemoverLocal(id);
            if (!removido)
                return NotFound();

            return NoContent();
        }
    }
}
