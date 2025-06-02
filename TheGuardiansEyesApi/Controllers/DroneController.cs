using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DroneController : ControllerBase
    {
        private readonly DroneService _droneService;

        public DroneController(DroneService droneService)
        {
            _droneService = droneService;
        }

        // GET: api/drone
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var drones = _droneService.ListarDrones();
                return drones.Count == 0 ? NoContent() : Ok(drones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao listar drones: {ex.Message}");
            }
        }

        // GET: api/drone/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var drone = _droneService.ObterPorId(id);
                return Ok(drone);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao obter drone: {ex.Message}");
            }
        }

        // POST: api/drone
        [HttpPost]
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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao cadastrar drone: {ex.Message}");
            }
        }

        // PUT: api/drone/{id}
        [HttpPut("{id}")]
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
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao atualizar drone: {ex.Message}");
            }
        }

        // DELETE: api/drone/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _droneService.RemoverDrone(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao remover drone: {ex.Message}");
            }
        }
    }
}
