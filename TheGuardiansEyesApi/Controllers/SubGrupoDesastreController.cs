using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubGrupoDesastreController : ControllerBase
    {
        private readonly SubGrupoDesastreService _service;

        public SubGrupoDesastreController(SubGrupoDesastreService service)
        {
            _service = service;
        }

        // GET: api/subgrupodesastre
        [HttpGet]
        public IActionResult Get()
        {
            var subgrupos = _service.ListarSubGrupos();
            return subgrupos.Count == 0 ? NoContent() : Ok(subgrupos);
        }

        // GET: api/subgrupodesastre/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var subgrupo = _service.ObterPorId(id);
            return subgrupo == null ? NotFound() : Ok(subgrupo);
        }

        // POST: api/subgrupodesastre
        [HttpPost]
        public IActionResult Post([FromBody] SubGrupoDesastreModel subgrupo)
        {
            if (string.IsNullOrWhiteSpace(subgrupo.Subgrupo) ||
                string.IsNullOrWhiteSpace(subgrupo.Tipo) ||
                string.IsNullOrWhiteSpace(subgrupo.SubTipo))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var criado = _service.CadastrarSubGrupo(subgrupo);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/subgrupodesastre/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubGrupoDesastreModel subgrupo)
        {
            if (subgrupo == null || subgrupo.Id != id)
                return BadRequest("Dados inconsistentes.");

            return _service.AtualizarSubGrupo(subgrupo) ? NoContent() : NotFound();
        }

        // DELETE: api/subgrupodesastre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.RemoverSubGrupo(id) ? NoContent() : NotFound();
        }
    }
}
