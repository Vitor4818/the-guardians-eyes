using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactoController : ControllerBase
    {
        private readonly ImpactoService impactoService;
        private readonly ILogger<ImpactoController> _logger;

        public ImpactoController(ImpactoService impactoService, ILogger<ImpactoController> logger)
        {
            this.impactoService = impactoService;
            _logger = logger;
        }

        private ObjectResult InternalError(Exception ex, string mensagemLog)
        {          
        _logger.LogError(ex, mensagemLog);
        return StatusCode(500, new ProblemDetails
        {
            Title = "Erro interno",
            Detail = ex.Message,
            Status = 500
        });
        }

        /// <summary>
        /// Lista todos os impactos cadastrados.
        /// </summary>
        /// <returns>Lista de impactos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os impactos", Description = "Retorna todos os impactos cadastrados no sistema.")]
        public IActionResult Get()
        {
            try
            {
                var impactos = impactoService.ListarImpactos();
                return impactos.Count == 0 ? NoContent() : Ok(impactos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar impactos.");
                return InternalError(ex, $"Erro ao atualizar impacto");

            }
        }

        /// <summary>
        /// Obtém um impacto pelo ID.
        /// </summary>
        /// <param name="id">ID do impacto.</param>
        /// <returns>Dados do impacto.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um impacto por ID", Description = "Retorna os dados de um impacto específico pelo seu ID.")]
        public IActionResult GetById(int id)
        {
            try
            {
                var impacto = impactoService.ObterPorId(id);
                if (impacto == null)
                    return NotFound("Impacto não encontrado.");
                return Ok(impacto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar impacto com ID {id}.");
                return InternalError(ex, $"Erro ao buscar pelo impacto com ID {id}.");

            }
        }

        /// <summary>
        /// Cadastra um novo impacto.
        /// </summary>
        /// <param name="impacto">Dados do impacto a ser cadastrado.</param>
        /// <returns>Impacto criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo impacto", Description = "Cadastra um novo impacto com os dados fornecidos.")]
        public IActionResult Post([FromBody] ImpactoModel impacto)
        {
          if (impacto == null || impacto.ImpactoClassificacaoId <= 0)
            return BadRequest("Impacto inválido. Verifique o campo ImpactoClassificacaoId.");

            try
            {
                var criado = impactoService.CadastrarImpacto(impacto);
                return CreatedAtAction(nameof(GetById), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro de validação ao cadastrar impacto.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao cadastrar impacto.");
                return InternalError(ex, $"Erro ao cadastrar Impacto");

            }
        }

        /// <summary>
        /// Atualiza os dados de um impacto existente.
        /// </summary>
        /// <param name="id">ID do impacto a ser atualizado.</param>
        /// <param name="impacto">Dados atualizados do impacto.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um impacto", Description = "Atualiza os dados de um impacto existente.")]
        public IActionResult Put(int id, [FromBody] ImpactoModel impacto)
        {
            if (impacto == null || impacto.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                impactoService.AtualizarImpacto(impacto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Impacto para atualização não encontrado.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao atualizar impacto.");
                return BadRequest("Dados inconsistentes: o ID informado na URL difere do corpo da requisição.");
            }
            catch (Exception ex)
            {
               return InternalError(ex, $"Erro ao atualizar impacto com ID {id}.");

            }
        }

        /// <summary>
        /// Remove um impacto do sistema.
        /// </summary>
        /// <param name="id">ID do impacto a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um impacto", Description = "Remove um impacto do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                 return BadRequest("ID inválido.");

            try
            {
                impactoService.RemoverImpacto(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Tentativa de exclusão de impacto inexistente.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Falha de regra de negócio ao excluir impacto.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
            _logger.LogError(ex, "Erro ao excluir impacto.");
                return InternalError(ex, $"Erro ao excluir impacto com ID {id}.");
            }
        }
    }
}
