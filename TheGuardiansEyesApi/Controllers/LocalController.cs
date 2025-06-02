using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using TheGuardiansEyesModel;

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

        [HttpGet]
        public IActionResult Get()
        {
            var locais = _localService.ListarLocais();
            return locais.Count == 0 ? NoContent() : Ok(locais);
        }

        [HttpGet("{id}")]
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

        [HttpGet("coordenadas")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
