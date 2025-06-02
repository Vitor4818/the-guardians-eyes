using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var desastres = _desastreService.ListarDesastres();
                return desastres.Count == 0 ? NoContent() : Ok(desastres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao listar desastres: {ex.Message}");
            }
        }

        // GET: api/desastre/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var desastre = _desastreService.ObterPorId(id);
                if (desastre == null)
                    return NotFound();

                return Ok(desastre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao obter desastre: {ex.Message}");
            }
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

            try
            {
                var criado = _desastreService.CadastrarDesastre(desastre);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Erro ao cadastrar desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao cadastrar desastre: {ex.Message}");
            }
        }

        // PUT: api/desastre/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DesastreModel desastre)
        {
            if (desastre == null || desastre.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                bool atualizado = _desastreService.AtualizarDesastre(desastre);
                if (!atualizado)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Erro ao atualizar desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar desastre: {ex.Message}");
            }
        }

        // DELETE: api/desastre/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool removido = _desastreService.RemoverDesastre(id);
                if (!removido)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Erro ao remover desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao remover desastre: {ex.Message}");
            }
        }
    }
}
