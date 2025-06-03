using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensoresController : ControllerBase
    {
        private readonly SensoresService _sensoresService;
        private readonly ILogger<SensoresController> _logger;

        public SensoresController(SensoresService sensoresService, ILogger<SensoresController> logger)
        {
            _sensoresService = sensoresService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os sensores cadastrados.
        /// </summary>
        /// <returns>Lista de sensores.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os sensores", Description = "Retorna todos os sensores cadastrados no sistema.")]
        public IActionResult Get()
        {
            var sensores = _sensoresService.ListarSensores();
            return sensores.Count == 0 ? NoContent() : Ok(sensores);
        }

        /// <summary>
        /// Obtém um sensor pelo ID.
        /// </summary>
        /// <param name="id">ID do sensor.</param>
        /// <returns>Dados do sensor.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um sensor por ID", Description = "Retorna os dados de um sensor específico pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var sensor = _sensoresService.ObterPorId(id);
                return Ok(sensor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar sensor por ID.");
                return StatusCode(500, "Erro interno ao buscar sensor.");
            }
        }

        /// <summary>
        /// Cadastra um novo sensor.
        /// </summary>
        /// <param name="sensor">Dados do sensor a ser cadastrado.</param>
        /// <returns>Sensor criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo sensor", Description = "Cadastra um novo sensor com os dados fornecidos.")]
        public IActionResult Post([FromBody] SensoresModel sensor)
        {
            if (sensor == null ||
                string.IsNullOrWhiteSpace(sensor.Chip) ||
                string.IsNullOrWhiteSpace(sensor.Modelo) ||
                string.IsNullOrWhiteSpace(sensor.Interface) ||
                string.IsNullOrWhiteSpace(sensor.Utilidade) ||
                string.IsNullOrWhiteSpace(sensor.Fabricante) ||
                string.IsNullOrWhiteSpace(sensor.Estado) ||
                string.IsNullOrWhiteSpace(sensor.Saida) ||
                string.IsNullOrWhiteSpace(sensor.TipoSaida) ||
                sensor.MediaTensaoRegistrada <= 0)
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            try
            {
                var criado = _sensoresService.CadastrarSensor(sensor);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar sensor.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Atualiza um sensor existente.
        /// </summary>
        /// <param name="id">ID do sensor a ser atualizado.</param>
        /// <param name="sensor">Dados atualizados do sensor.</param>
        /// <returns>Sensor atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um sensor", Description = "Atualiza os dados de um sensor existente.")]
        public IActionResult Put(int id, [FromBody] SensoresModel sensor)
        {
            if (sensor == null || sensor.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                var atualizado = _sensoresService.AtualizarSensor(sensor);
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
                _logger.LogError(ex, "Erro inesperado ao atualizar sensor.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove um sensor pelo ID.
        /// </summary>
        /// <param name="id">ID do sensor a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um sensor", Description = "Remove um sensor do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sensoresService.RemoverSensor(id);
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
                _logger.LogError(ex, "Erro inesperado ao excluir sensor.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}
