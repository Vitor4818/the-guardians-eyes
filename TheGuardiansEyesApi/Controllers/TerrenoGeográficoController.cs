using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrenoGeograficoController : ControllerBase
    {
        private readonly TerrenoGeograficoService terrenoService;
        private readonly ILogger<TerrenoGeograficoController> _logger;

        public TerrenoGeograficoController(TerrenoGeograficoService terrenoService, ILogger<TerrenoGeograficoController> logger)
        {
            this.terrenoService = terrenoService;
            _logger = logger;
        }

        // GET: api/terrenogeografico
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var terrenos = terrenoService.ListarTerrenos();
                return terrenos.Count == 0 ? NoContent() : Ok(terrenos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar terrenos geográficos.");
                return StatusCode(500, "Erro interno ao buscar terrenos.");
            }
        }

        // GET: api/terrenogeografico/{id}
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    try
    {
        var terreno = terrenoService.ObterPorId(id);
        return Ok(terreno);
    }
    catch (KeyNotFoundException ex)
    {
        _logger.LogWarning(ex, "Terreno geográfico não encontrado.");
        return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro ao buscar terreno geográfico por ID.");
        return StatusCode(500, "Erro interno ao buscar o terreno.");
    }
}
    }
}
