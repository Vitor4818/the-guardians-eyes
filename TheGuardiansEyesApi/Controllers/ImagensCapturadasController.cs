using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using System.Threading.Tasks;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagensCapturadasController : ControllerBase
    {
        private readonly ImagensCapturadasService _imagemService;

        public ImagensCapturadasController(ImagensCapturadasService imagemService)
        {
            _imagemService = imagemService;
        }

        // GET: api/imagenscapturadas
        [HttpGet]
        public IActionResult Get()
        {
            var imagens = _imagemService.ListarImagens();
            return imagens.Count == 0 ? NoContent() : Ok(imagens);
        }

        // GET: api/imagenscapturadas/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var imagem = _imagemService.ObterPorId(id);
            return imagem == null ? NotFound() : Ok(imagem);
        }

        // POST: api/imagenscapturadas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImagensCapturadasModel imagem)
        {
            if (imagem == null || imagem.IdDrone <= 0 || string.IsNullOrWhiteSpace(imagem.Hospedagem))
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            var criada = await _imagemService.CadastrarImagemAsync(imagem);
            return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
        }

        // PUT: api/imagenscapturadas/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ImagensCapturadasModel imagem)
        {
            if (imagem == null || imagem.Id != id)
                return BadRequest("Dados inconsistentes.");

            return _imagemService.AtualizarImagem(imagem) ? NoContent() : NotFound();
        }

        // DELETE: api/imagenscapturadas/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _imagemService.RemoverImagem(id) ? NoContent() : NotFound();
        }
    }
}
