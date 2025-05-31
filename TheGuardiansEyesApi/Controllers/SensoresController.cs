using Microsoft.AspNetCore.Mvc;
using TheGuardiansEyesModel;
using TheGuardiansEyesBusiness;

namespace TheGuardiansEyesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensoresController : ControllerBase
    {
        private readonly SensoresService _sensoresService;

        public SensoresController(SensoresService sensoresService)
        {
            _sensoresService = sensoresService;
        }

        // GET: api/sensores
        [HttpGet]
        public IActionResult Get()
        {
            var sensores = _sensoresService.ListarSensores();
            return sensores.Count == 0 ? NoContent() : Ok(sensores);
        }

        // GET: api/sensores/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var sensor = _sensoresService.ObterPorId(id);
            return sensor == null ? NotFound() : Ok(sensor);
        }

        // POST: api/sensores
        [HttpPost]
        public IActionResult Post([FromBody] SensoresModel sensor)
        {
            if (sensor == null ||
                string.IsNullOrWhiteSpace(sensor.Chip) ||
                string.IsNullOrWhiteSpace(sensor.Modelo) ||
                string.IsNullOrWhiteSpace(sensor.Interface) ||
                string.IsNullOrWhiteSpace(sensor.Utilidade) ||
                string.IsNullOrWhiteSpace(sensor.Fabricante) ||
                string.IsNullOrWhiteSpace(sensor.Estado) ||
                string.IsNullOrWhiteSpace(sensor.Saida) ||
                string.IsNullOrWhiteSpace(sensor.TipoSaida) ||
                sensor.MediaTensaoRegistrada <= 0)
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            var criado = _sensoresService.CadastrarSensor(sensor);
            return CreatedAtAction(nameof(Get), new { id = criado.Id }, criado);
        }

        // PUT: api/sensores/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SensoresModel sensor)
        {
            if (sensor == null || sensor.Id != id)
                return BadRequest("Dados inconsistentes.");

            return _sensoresService.AtualizarSensor(sensor) ? NoContent() : NotFound();
        }

        // DELETE: api/sensores/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _sensoresService.RemoverSensor(id) ? NoContent() : NotFound();
        }
    }
}
