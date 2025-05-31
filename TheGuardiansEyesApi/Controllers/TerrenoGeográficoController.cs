using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TerrenoGeograficoController : ControllerBase
    {
        private readonly TerrenoGeograficoService terrenoService;

        public TerrenoGeograficoController(TerrenoGeograficoService terrenoService)
        {
            this.terrenoService = terrenoService;
        }

        // GET: api/terrenogeografico
        [HttpGet]
        public IActionResult Get()
        {
            var terrenos = terrenoService.ListarTerrenos();
            return terrenos.Count == 0 ? NoContent() : Ok(terrenos);
        }

        // GET: api/terrenogeografico/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var terreno = terrenoService.ObterPorId(id);
            return terreno == null ? NotFound() : Ok(terreno);
        }

    }
}
