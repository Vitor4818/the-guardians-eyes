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

        public ImpactoClassificacaoController(ImpactoClassificacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lista = _service.ListarClassificacoes();
            return lista.Count == 0 ? NoContent() : Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _service.ObterPorId(id);
            return item == null ? NotFound() : Ok(item);
        }

    }
}
