using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;
using Swashbuckle.AspNetCore.Annotations;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly LocalService _localService;
        private readonly ILogger<LocalController> _logger;

        public LocalController(LocalService localService, ILogger<LocalController> logger)
        {
            _localService = localService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os locais cadastrados.
        /// </summary>
        /// <returns>Lista de locais.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os locais", Description = "Retorna todos os locais cadastrados no sistema.")]
        public IActionResult Get()
        {
            var locais = _localService.ListarLocais();
            return locais.Count == 0 ? NoContent() : Ok(locais);
        }

        /// <summary>
        /// Obtém um local pelo ID.
        /// </summary>
        /// <param name="id">ID do local.</param>
        /// <returns>Dados do local.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um local por ID", Description = "Retorna os dados de um local específico pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var local = _localService.ObterPorId(id);
                return Ok(local);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar local por ID.");
                return StatusCode(500, "Erro interno ao buscar local.");
            }
        }

        /// <summary>
        /// Obtém um local pelas coordenadas de latitude e longitude.
        /// </summary>
        /// <param name="latitude">Latitude do local.</param>
        /// <param name="longitude">Longitude do local.</param>
        /// <returns>Dados do local.</returns>
        [HttpGet("coordenadas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um local por coordenadas", Description = "Retorna os dados de um local específico pelas coordenadas de latitude e longitude.")]
        public IActionResult GetPorCoordenadas([FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                var local = _localService.ObterPorCoordenadas(latitude, longitude);
                return Ok(local);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar local por coordenadas.");
                return StatusCode(500, "Erro interno ao buscar local.");
            }
        }

        /// <summary>
        /// Cadastra um novo local.
        /// </summary>
        /// <param name="local">Dados do local a ser cadastrado.</param>
        /// <returns>Local criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo local", Description = "Cadastra um novo local com os dados fornecidos.")]
        public IActionResult Post([FromBody] LocalModel local)
        {
            if (local == null || local.Latitude == 0 || local.Longitude == 0)
                return BadRequest("Latitude e Longitude devem ser fornecidas e válidas.");

            try
            {
                var criado = _localService.CadastrarLocal(local);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar local.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Atualiza os dados de um local existente.
        /// </summary>
        /// <param name="id">ID do local a ser atualizado.</param>
        /// <param name="local">Dados atualizados do local.</param>
        /// <returns>Local atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um local", Description = "Atualiza os dados de um local existente.")]
        public IActionResult Put(int id, [FromBody] LocalModel local)
        {
            if (local == null || local.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                var atualizado = _localService.AtualizarLocal(local);
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
                _logger.LogError(ex, "Erro inesperado ao atualizar local.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove um local pelo ID.
        /// </summary>
        /// <param name="id">ID do local a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um local", Description = "Remove um local do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            try
            {
                _localService.RemoverLocal(id);
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
                _logger.LogError(ex, "Erro inesperado ao excluir local.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}
