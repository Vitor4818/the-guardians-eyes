using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesastreController : ControllerBase
    {
        private readonly DesastreService _desastreService;

        public DesastreController(DesastreService desastreService)
        {
            _desastreService = desastreService;
        }

        // GET: api/desastre
        [HttpGet]
        public IActionResult Get()
        {
            var desastres = _desastreService.ListarDesastres();
            return desastres.Count == 0 ? NoContent() : Ok(desastres);
        }

        // GET: api/desastre/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var desastre = _desastreService.ObterPorId(id);
            return desastre == null ? NotFound() : Ok(desastre);
        }

        // POST: api/desastre
        [HttpPost]
        public IActionResult Post([FromBody] DesastreModel desastre)
        {
            if (desastre == null ||
                desastre.IdLocal <= 0 ||
                desastre.Impacto <= 0 ||
                desastre.IdGrupoDesastre <= 0 ||
                desastre.IdUsuario <= 0 ||
                string.IsNullOrWhiteSpace(desastre.DataOcorrencia))
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            var criado = _desastreService.CadastrarDesastre(desastre);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/desastre/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DesastreModel desastre)
        {
            if (desastre == null || desastre.Id != id)
                return BadRequest("Dados inconsistentes.");

            return _desastreService.AtualizarDesastre(desastre) ? NoContent() : NotFound();
        }

        // DELETE: api/desastre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _desastreService.RemoverDesastre(id) ? NoContent() : NotFound();
        }
    }
}
