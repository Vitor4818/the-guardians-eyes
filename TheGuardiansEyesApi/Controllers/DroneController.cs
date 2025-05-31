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
            var drones = _droneService.ListarDrones();
            return drones.Count == 0 ? NoContent() : Ok(drones);
        }

        // GET: api/drone/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var drone = _droneService.ObterPorId(id);
            return drone == null ? NotFound() : Ok(drone);
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

            var criado = _droneService.CadastrarDrone(drone);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/drone/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DroneModel drone)
        {
            if (drone == null || drone.Id != id)
                return BadRequest("Dados inconsistentes.");

            return _droneService.AtualizarDrone(drone) ? NoContent() : NotFound();
        }

        // DELETE: api/drone/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _droneService.RemoverDrone(id) ? NoContent() : NotFound();
        }
    }
}
