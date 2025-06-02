using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

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

        [HttpGet]
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
                return StatusCode(500, "Erro interno ao listar classificações.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
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
                return StatusCode(500, "Erro interno ao buscar classificação.");
            }
        }
    }
}
