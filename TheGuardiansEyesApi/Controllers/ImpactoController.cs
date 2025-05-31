using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactoController : ControllerBase
    {
        private readonly ImpactoService impactoService;

        public ImpactoController(ImpactoService impactoService)
        {
            this.impactoService = impactoService;
        }

        // GET: api/impacto
        [HttpGet]
        public IActionResult Get()
        {
            var impactos = impactoService.ListarImpactos();
            return impactos.Count == 0 ? NoContent() : Ok(impactos);
        }

        // GET: api/impacto/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var impacto = impactoService.ObterPorId(id);
            return impacto == null ? NotFound() : Ok(impacto);
        }

        // POST: api/impacto
        [HttpPost]
        public IActionResult Post([FromBody] ImpactoModel impacto)
        {
            if (impacto == null || impacto.ImpactoClassificacaoId <= 0)
                return BadRequest("Dados inválidos.");

            var criado = impactoService.CadastrarImpacto(impacto);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/impacto/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ImpactoModel impacto)
        {
            if (impacto == null || impacto.Id != id)
                return BadRequest("Dados inconsistentes.");

            return impactoService.AtualizarImpacto(impacto) ? NoContent() : NotFound();
        }

        // DELETE: api/impacto/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return impactoService.RemoverImpacto(id) ? NoContent() : NotFound();
        }
    }
}
