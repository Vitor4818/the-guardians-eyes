using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;

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

        [HttpGet]
        public IActionResult Get()
        {
            var sensores = _sensoresService.ListarSensores();
            return sensores.Count == 0 ? NoContent() : Ok(sensores);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
