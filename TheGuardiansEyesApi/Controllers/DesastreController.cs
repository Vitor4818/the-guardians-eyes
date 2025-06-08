using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesastreController : ControllerBase
    {
        private readonly DesastreService _desastreService;
        private readonly ILogger<DesastreController> _logger;

        public DesastreController(DesastreService desastreService, ILogger<DesastreController> logger)
        {
            _desastreService = desastreService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os desastres cadastrados.
        /// </summary>
        /// <returns>Lista de desastres.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os desastres", Description = "Retorna todos os desastres cadastrados no sistema.")]
        public IActionResult Get()
        {
            try
            {
                var desastres = _desastreService.ListarDesastres();
                return desastres.Count == 0 ? NoContent() : Ok(desastres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar desastres.");
                return StatusCode(500, "Erro interno ao listar desastres.");
            }
        }

        /// <summary>
        /// Obtém um desastre pelo ID.
        /// </summary>
        /// <param name="id">ID do desastre.</param>
        /// <returns>Dados do desastre.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um desastre por ID", Description = "Retorna os dados de um desastre específico pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var desastre = _desastreService.ObterPorId(id);
                if (desastre == null)
                {
                    return NotFound("Desastre não encontrado.");
                }
                return Ok(desastre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter desastre por ID.");
                return StatusCode(500, "Erro interno ao buscar desastre.");
            }
        }

        /// <summary>
        /// Cadastra um novo desastre.
        /// </summary>
        /// <param name="desastre">Dados do desastre a ser cadastrado.</param>
        /// <returns>Desastre criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo desastre", Description = "Cadastra um novo desastre com os dados fornecidos.")]
        public IActionResult Post([FromBody] DesastreModel desastre)
        {
            if (desastre == null ||
                desastre.IdLocal <= 0 ||
                desastre.IdImpactoClassificacao <= 0 ||
                desastre.IdGrupoDesastre <= 0 ||
                desastre.IdUsuario <= 0 ||
                string.IsNullOrWhiteSpace(desastre.DataOcorrencia))
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            try
            {
                var criado = _desastreService.CadastrarDesastre(desastre);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao cadastrar desastre.");
                return BadRequest($"Erro ao cadastrar desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar desastre.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Atualiza os dados de um desastre existente.
        /// </summary>
        /// <param name="id">ID do desastre a ser atualizado.</param>
        /// <param name="desastre">Dados atualizados do desastre.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um desastre", Description = "Atualiza os dados de um desastre existente.")]
        public IActionResult Put(int id, [FromBody] DesastreModel desastre)
        {
            if (desastre == null || desastre.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                bool atualizado = _desastreService.AtualizarDesastre(desastre);
                if (!atualizado)
                    return NotFound("Desastre não encontrado para atualização.");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao atualizar desastre.");
                return BadRequest($"Erro ao atualizar desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar desastre.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove um desastre do sistema.
        /// </summary>
        /// <param name="id">ID do desastre a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um desastre", Description = "Remove um desastre do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool removido = _desastreService.RemoverDesastre(id);
                if (!removido)
                    return NotFound("Desastre não encontrado para exclusão.");

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao excluir desastre.");
                return BadRequest($"Erro ao excluir desastre: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao excluir desastre.");
                return StatusCode(500, "Erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}
