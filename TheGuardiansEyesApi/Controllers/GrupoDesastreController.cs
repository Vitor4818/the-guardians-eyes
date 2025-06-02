using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoDesastreController : ControllerBase
    {
        private readonly GrupoDesastreService _grupoService;

        public GrupoDesastreController(GrupoDesastreService grupoService)
        {
            _grupoService = grupoService;
        }

        // GET: api/grupodesastre
        [HttpGet]
        public IActionResult Get()
        {
            var grupos = _grupoService.ListarGrupos();
            return grupos.Count == 0 ? NoContent() : Ok(grupos);
        }

        // GET: api/grupodesastre/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var grupo = _grupoService.ObterPorId(id);
                return Ok(grupo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/grupodesastre
        [HttpPost]
        public IActionResult Post([FromBody] GrupoDesastreModel grupo)
        {
            if (string.IsNullOrWhiteSpace(grupo.NomeGrupo))
                return BadRequest("O nome do grupo é obrigatório.");

            try
            {
                var criado = _grupoService.CadastrarGrupo(grupo);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/grupodesastre/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GrupoDesastreModel grupo)
        {
            if (grupo == null || grupo.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                var atualizado = _grupoService.AtualizarGrupo(grupo);
                return atualizado ? NoContent() : NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/grupodesastre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var removido = _grupoService.RemoverGrupo(id);
                return removido ? NoContent() : NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
