using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TheGuardiansEyesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaLocalizadaController : ControllerBase
    {
        private readonly PessoaLocalizadaService _service;
        private readonly ILogger<PessoaLocalizadaController> _logger;

        public PessoaLocalizadaController(PessoaLocalizadaService service, ILogger<PessoaLocalizadaController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Lista todas as pessoas localizadas cadastradas.
        /// </summary>
        /// <returns>Lista de pessoas localizadas.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista todas as pessoas localizadas",
            Description = "Retorna todas as pessoas localizadas cadastradas no sistema."
        )]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pessoas = await _service.ListarPessoasLocalizadasAsync();
                if (pessoas == null || !pessoas.Any())
                    return NoContent();

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pessoas localizadas");
                return StatusCode(500, "Erro interno ao buscar pessoas localizadas.");
            }
        }

        /// <summary>
        /// Lista as pessoas localizadas nas últimas 48 horas.
        /// </summary>
        /// <returns>Lista de pessoas localizadas nas últimas 48 horas.</returns>
        [HttpGet("ultimas-48h")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista pessoas localizadas nas últimas 48 horas",
            Description = "Retorna todas as pessoas localizadas cadastradas nas últimas 48 horas."
        )]
        public async Task<IActionResult> GetUltimas48Horas()
        {
            try
            {
                var pessoas = await _service.ListarPessoasUltimas48HorasAsync();
                if (pessoas == null || !pessoas.Any())
                    return NoContent();

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pessoas localizadas nas últimas 48 horas");
                return StatusCode(500, "Erro interno ao buscar pessoas localizadas nas últimas 48 horas.");
            }
        }
    }
}
