using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Lista todos os subgrupos de desastre cadastrados.
        /// </summary>
        /// <returns>Lista de subgrupos de desastre.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os subgrupos de desastre", Description = "Retorna todos os subgrupos de desastre cadastrados no sistema.")]
        public IActionResult Get()
        {
            var subgrupos = _service.ListarSubGrupos();
            return subgrupos.Count == 0 ? NoContent() : Ok(subgrupos);
        }

        /// <summary>
        /// Obtém um subgrupo de desastre pelo ID.
        /// </summary>
        /// <param name="id">ID do subgrupo de desastre.</param>
        /// <returns>Dados do subgrupo de desastre.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um subgrupo de desastre por ID", Description = "Retorna os dados de um subgrupo de desastre específico pelo seu ID.")]
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

        /// <summary>
        /// Cadastra um novo subgrupo de desastre.
        /// </summary>
        /// <param name="subgrupo">Dados do subgrupo de desastre a ser cadastrado.</param>
        /// <returns>Subgrupo de desastre criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo subgrupo de desastre", Description = "Cadastra um novo subgrupo de desastre com os dados fornecidos.")]
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

        /// <summary>
        /// Atualiza os dados de um subgrupo de desastre existente.
        /// </summary>
        /// <param name="id">ID do subgrupo a ser atualizado.</param>
        /// <param name="subgrupo">Dados atualizados do subgrupo de desastre.</param>
        /// <returns>Subgrupo atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um subgrupo de desastre", Description = "Atualiza os dados de um subgrupo de desastre existente.")]
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

        /// <summary>
        /// Remove um subgrupo de desastre do sistema.
        /// </summary>
        /// <param name="id">ID do subgrupo a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um subgrupo de desastre", Description = "Remove um subgrupo de desastre do sistema com base no ID informado.")]
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
