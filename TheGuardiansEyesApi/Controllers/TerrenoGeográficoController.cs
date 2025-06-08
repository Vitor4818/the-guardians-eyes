using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace TheGuardiansEyesApi.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Lista todos os terrenos geográficos cadastrados.
        /// </summary>
        /// <returns>Lista de terrenos geográficos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Lista todos os terrenos geográficos", Description = "Retorna todos os terrenos geográficos cadastrados no sistema.")]
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

        /// <summary>
        /// Obtém um terreno geográfico pelo ID.
        /// </summary>
        /// <param name="id">ID do terreno geográfico.</param>
        /// <returns>Dados do terreno geográfico.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um terreno geográfico por ID", Description = "Retorna os dados de um terreno geográfico específico pelo seu ID.")]
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
