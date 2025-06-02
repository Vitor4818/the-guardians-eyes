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
        private readonly DesastreService _desastreService;
        private readonly LocalService _localService;

        public ImagensCapturadasController(
            ImagensCapturadasService imagemService,
            DesastreService desastreService,
            LocalService localService)
        {
            _imagemService = imagemService;
            _desastreService = desastreService;
            _localService = localService;
        }

// GET: api/imagenscapturadas
[HttpGet]
public IActionResult Get()
{
    try
    {
        var imagens = _imagemService.ListarImagens();
        return imagens.Count == 0 ? NoContent() : Ok(imagens);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensagem = "Erro ao listar imagens.", detalhe = ex.Message });
    }
}

// GET: api/imagenscapturadas/{id}
[HttpGet("{id}")]
public IActionResult Get(int id)
{
    try
    {
        var imagem = _imagemService.ObterPorId(id);
        return Ok(imagem);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new { mensagem = ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensagem = "Erro ao buscar imagem por ID.", detalhe = ex.Message });
    }
}




        // POST: api/imagenscapturadas
     // POST: api/imagenscapturadas
       [HttpPost]
public async Task<IActionResult> Post([FromBody] ImagensCapturadasModel imagem)
{
    if (imagem == null || imagem.IdDrone <= 0 || string.IsNullOrWhiteSpace(imagem.Hospedagem))
        return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");

    // Buscar local pelo IdLocal
    var local = _localService.ObterPorId(imagem.IdLocal);
    if (local == null)
        return BadRequest("Local inválido.");

    // Verificar se local tem latitude e longitude válidas
    if (local.Latitude == 0 || local.Longitude == 0)
        return BadRequest("Local não possui latitude e longitude válidas.");

    // Buscar desastre mais próximo usando lat/lon do local
    var desastreProximo = _desastreService.ObterDesastreMaisProximo(local.Latitude, local.Longitude);

    if (desastreProximo == null)
        return NotFound("Nenhum desastre próximo encontrado para a localização do local.");

    // Atribuir IdDesastre e IdLocal na imagem
    imagem.IdDesastre = desastreProximo.Id;
    imagem.IdLocal = desastreProximo.IdLocal;

    var criada = await _imagemService.CadastrarImagemAsync(imagem);
    return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
}






[HttpPut("{id}")]
public IActionResult Put(int id, [FromBody] ImagensCapturadasModel imagem)
{
    if (imagem == null || imagem.Id != id)
        return BadRequest("Dados inconsistentes.");

    try
    {
        _imagemService.AtualizarImagem(imagem);
        return NoContent();
    }
    catch (KeyNotFoundException)
    {
        return NotFound("Imagem não encontrada.");
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}

[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    try
    {
        _imagemService.RemoverImagem(id);
        return NoContent();
    }
    catch (KeyNotFoundException)
    {
        return NotFound("Imagem não encontrada.");
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}
    }
}
