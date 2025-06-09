using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactoClassificacaoController : ControllerBase
    {
        private readonly ImpactoClassificacaoService _service;
        private readonly ILogger<ImpactoClassificacaoController> _logger;

        public ImpactoClassificacaoController(ImpactoClassificacaoService service, ILogger<ImpactoClassificacaoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Lista todas as classificações de impacto cadastradas.
        /// </summary>
        /// <returns>Lista de classificações de impacto.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Lista todas as classificações de impacto",
            Description = "Retorna todas as classificações de impacto cadastradas no sistema."
        )]
        public IActionResult Get()
        {
            try
            {
                var lista = _service.ListarClassificacoes();
                return lista.Count == 0 ? NoContent() : Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar classificações de impacto.");
               return StatusCode(500, new ProblemDetails
                {
                    Title = "Erro interno",
                    Detail = ex.Message,
                    Status = 500
                });
            }
        }

        /// <summary>
        /// Obtém uma classificação de impacto pelo ID.
        /// </summary>
        /// <param name="id">ID da classificação de impacto.</param>
        /// <returns>Dados da classificação de impacto.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Busca uma classificação de impacto por ID",
            Description = "Retorna os dados de uma classificação de impacto específica pelo seu ID."
        )]
        public IActionResult Get(int id)
        {
            if (id <= 0)
            return BadRequest("ID inválido.");
            
            try
            {
                var item = _service.ObterPorId(id);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Classificação de impacto com ID {id} não encontrada.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar classificação de impacto com ID {id}.");
                                return StatusCode(500, new ProblemDetails
                {
                    Title = "Erro interno",
                    Detail = ex.Message,
                    Status = 500
                });
            }
        }
    }
}
