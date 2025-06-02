using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using Microsoft.EntityFrameworkCore;
using TheGuardiansEyesModel;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubGrupoDesastreController : ControllerBase
    {
        private readonly SubGrupoDesastreService _service;
        private readonly ILogger<SubGrupoDesastreController> _logger;

        public SubGrupoDesastreController(SubGrupoDesastreService service, ILogger<SubGrupoDesastreController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var subgrupos = _service.ListarSubGrupos();
            return subgrupos.Count == 0 ? NoContent() : Ok(subgrupos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var subgrupo = _service.ObterPorId(id);
                return Ok(subgrupo);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar subgrupo de desastre por ID.");
                return StatusCode(500, "Erro interno ao buscar subgrupo.");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubGrupoDesastreModel subgrupo)
        {
            if (subgrupo == null ||
                string.IsNullOrWhiteSpace(subgrupo.Subgrupo) ||
                string.IsNullOrWhiteSpace(subgrupo.Tipo) ||
                string.IsNullOrWhiteSpace(subgrupo.SubTipo) ||
                subgrupo.GrupoDesastreId <= 0)
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            try
            {
                var criado = _service.CadastrarSubGrupo(subgrupo);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar subgrupo.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SubGrupoDesastreModel subgrupo)
        {
            if (subgrupo == null || subgrupo.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                var atualizado = _service.AtualizarSubGrupo(subgrupo);
                return Ok(atualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar subgrupo.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.RemoverSubGrupo(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao excluir subgrupo.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}