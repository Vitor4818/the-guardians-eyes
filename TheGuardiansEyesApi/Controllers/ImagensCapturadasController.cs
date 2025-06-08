using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
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
        private readonly ILogger<ImagensCapturadasController> _logger;

        public ImagensCapturadasController(
            ImagensCapturadasService imagemService,
            DesastreService desastreService,
            LocalService localService,
            ILogger<ImagensCapturadasController> logger)
        {
            _imagemService = imagemService;
            _desastreService = desastreService;
            _localService = localService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todas as imagens capturadas.
        /// </summary>
        /// <returns>Lista de imagens capturadas.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Lista todas as imagens capturadas", Description = "Retorna todas as imagens capturadas cadastradas no sistema.")]
        public IActionResult Get()
        {
            try
            {
                var imagens = _imagemService.ListarImagens();
                return imagens.Count == 0 ? NoContent() : Ok(imagens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar imagens.");
                return StatusCode(500, new { mensagem = "Erro ao listar imagens.", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Obtém uma imagem capturada pelo ID.
        /// </summary>
        /// <param name="id">ID da imagem capturada.</param>
        /// <returns>Dados da imagem capturada.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca uma imagem capturada por ID", Description = "Retorna os dados de uma imagem capturada específica pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var imagem = _imagemService.ObterPorId(id);
                return Ok(imagem);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Imagem não encontrada para ID {Id}.", id);
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar imagem por ID.");
                return StatusCode(500, new { mensagem = "Erro ao buscar imagem por ID.", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Cadastra uma nova imagem capturada.
        /// </summary>
        /// <param name="imagem">Dados da imagem capturada a ser cadastrada.</param>
        /// <returns>Imagem capturada criada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra uma nova imagem capturada", Description = "Cadastra uma nova imagem capturada com os dados fornecidos.")]
        public async Task<IActionResult> Post([FromBody] ImagensCapturadasModel imagem)
        {
            if (imagem == null || imagem.IdDrone <= 0 || string.IsNullOrWhiteSpace(imagem.Hospedagem))
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");

            try
            {
                var local = _localService.ObterPorId(imagem.IdLocal);
                if (local == null)
                    return BadRequest("Local inválido.");

                if (local.Latitude == 0 || local.Longitude == 0)
                    return BadRequest("Local não possui latitude e longitude válidas.");

                var desastreProximo = _desastreService.ObterDesastreMaisProximo(local.Latitude, local.Longitude);
                if (desastreProximo == null)
                    return NotFound("Nenhum desastre próximo encontrado para a localização do local.");

                imagem.IdDesastre = desastreProximo.Id;

                var criada = await _imagemService.CadastrarImagemAsync(imagem);
                return CreatedAtAction(nameof(Get), new { id = criada.Id }, criada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar imagem capturada.");
                return StatusCode(500, new { mensagem = "Erro ao cadastrar imagem capturada.", detalhe = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma imagem capturada existente.
        /// </summary>
        /// <param name="id">ID da imagem capturada a ser atualizada.</param>
        /// <param name="imagem">Dados atualizados da imagem capturada.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza uma imagem capturada", Description = "Atualiza os dados de uma imagem capturada existente.")]
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
                _logger.LogWarning(ex, "Erro ao atualizar imagem capturada.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar imagem capturada.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove uma imagem capturada pelo ID.
        /// </summary>
        /// <param name="id">ID da imagem capturada a ser removida.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove uma imagem capturada", Description = "Remove uma imagem capturada existente pelo seu ID.")]
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
                _logger.LogWarning(ex, "Erro ao remover imagem capturada.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao remover imagem capturada.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}
