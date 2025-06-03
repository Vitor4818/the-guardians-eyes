using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly DroneService _droneService;
        private readonly ILogger<DroneController> _logger;

        public DroneController(DroneService droneService, ILogger<DroneController> logger)
        {
            _droneService = droneService;
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os drones cadastrados.
        /// </summary>
        /// <returns>Lista de drones.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Lista todos os drones", Description = "Retorna todos os drones cadastrados no sistema.")]
        public IActionResult Get()
        {
            try
            {
                var drones = _droneService.ListarDrones();
                return drones.Count == 0 ? NoContent() : Ok(drones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao listar drones.");
                return StatusCode(500, $"Erro interno ao listar drones: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um drone pelo ID.
        /// </summary>
        /// <param name="id">ID do drone.</param>
        /// <returns>Dados do drone.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Busca um drone por ID", Description = "Retorna os dados de um drone específico pelo seu ID.")]
        public IActionResult Get(int id)
        {
            try
            {
                var drone = _droneService.ObterPorId(id);
                return Ok(drone);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Drone não encontrado.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao obter drone.");
                return StatusCode(500, $"Erro interno ao obter drone: {ex.Message}");
            }
        }

        /// <summary>
        /// Cadastra um novo drone.
        /// </summary>
        /// <param name="drone">Dados do drone a ser cadastrado.</param>
        /// <returns>Drone criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo drone", Description = "Cadastra um novo drone com os dados fornecidos.")]
        public IActionResult Post([FromBody] DroneModel drone)
        {
            if (drone == null ||
                string.IsNullOrWhiteSpace(drone.Fabricante) ||
                string.IsNullOrWhiteSpace(drone.Modelo) ||
                string.IsNullOrWhiteSpace(drone.TempoVoo) ||
                drone.DistanciaMaxima <= 0 ||
                drone.VelocidadeMaxima <= 0 ||
                string.IsNullOrWhiteSpace(drone.Camera) ||
                drone.Peso <= 0)
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            try
            {
                var criado = _droneService.CadastrarDrone(drone);
                return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao cadastrar drone.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao cadastrar drone.");
                return StatusCode(500, $"Erro interno ao cadastrar drone: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza os dados de um drone existente.
        /// </summary>
        /// <param name="id">ID do drone a ser atualizado.</param>
        /// <param name="drone">Dados atualizados do drone.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Atualiza um drone", Description = "Atualiza os dados de um drone existente.")]
        public IActionResult Put(int id, [FromBody] DroneModel drone)
        {
            if (drone == null || drone.Id != id)
                return BadRequest("Dados inconsistentes.");

            try
            {
                _droneService.AtualizarDrone(drone);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Drone não encontrado para atualização.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao atualizar drone.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao atualizar drone.");
                return StatusCode(500, $"Erro interno ao atualizar drone: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove um drone do sistema.
        /// </summary>
        /// <param name="id">ID do drone a ser removido.</param>
        /// <returns>Resposta sem conteúdo.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Remove um drone", Description = "Remove um drone do sistema com base no ID informado.")]
        public IActionResult Delete(int id)
        {
            try
            {
                _droneService.RemoverDrone(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Drone não encontrado para exclusão.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Erro ao excluir drone.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno ao excluir drone.");
                return StatusCode(500, $"Erro interno ao remover drone: {ex.Message}");
            }
        }
    }
}
