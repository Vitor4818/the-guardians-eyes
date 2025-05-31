using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoDesastreController : ControllerBase
    {
        private readonly GrupoDesastreService grupoService;

        public GrupoDesastreController(GrupoDesastreService grupoService)
        {
            this.grupoService = grupoService;
        }

        // GET: api/grupodesastre
        [HttpGet]
        public IActionResult Get()
        {
            var grupos = grupoService.ListarGrupos();
            return grupos.Count == 0 ? NoContent() : Ok(grupos);
        }

        // GET: api/grupodesastre/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var grupo = grupoService.ObterPorId(id);
            return grupo == null ? NotFound() : Ok(grupo);
        }

        // POST: api/grupodesastre
        [HttpPost]
        public IActionResult Post([FromBody] GrupoDesastreModel grupo)
        {
            if (string.IsNullOrWhiteSpace(grupo.NomeGrupo))
                return BadRequest("O nome do grupo é obrigatório.");

            var criado = grupoService.CadastrarGrupo(grupo);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/grupodesastre/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GrupoDesastreModel grupo)
        {
            if (grupo == null || grupo.Id != id)
                return BadRequest("Dados inconsistentes.");

            return grupoService.AtualizarGrupo(grupo) ? NoContent() : NotFound();
        }

        // DELETE: api/grupodesastre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return grupoService.RemoverGrupo(id) ? NoContent() : NotFound();
        }
    }
}
