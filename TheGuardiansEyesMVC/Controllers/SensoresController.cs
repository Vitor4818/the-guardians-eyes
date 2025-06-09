using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using TheGuardiansEyesModel;

namespace TheGuardiansEyesMVC.Controllers
{
    public class SensoresController : Controller
    {
        private readonly HttpClient _httpClient;

        public SensoresController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        // GET: Sensores
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Sensores");

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                ViewBag.Mensagem = "Nenhum sensor cadastrado.";
                return View(new List<SensoresModel>());
            }

            response.EnsureSuccessStatusCode();

            var sensores = await response.Content.ReadFromJsonAsync<List<SensoresModel>>();

            return View(sensores);
        }

        // GET: Sensores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sensor = await GetSensorByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return View(sensor);
        }

        // GET: Sensores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sensores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensoresModel sensor)
        {
            if (!ModelState.IsValid)
                return View(sensor);

            var response = await PostSensorAsync(sensor);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar o sensor: {error}");
            return View(sensor);
        }

        // GET: Sensores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await GetSensorByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return View(sensor);
        }

        // POST: Sensores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SensoresModel sensor)
        {
            if (id != sensor.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(sensor);

            var response = await PutSensorAsync(id, sensor);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao atualizar o sensor: {error}");
            return View(sensor);
        }

        // GET: Sensores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await GetSensorByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return View(sensor);
        }

        // POST: Sensores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Sensores/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao excluir o sensor: {error}");
            return RedirectToAction(nameof(Delete), new { id });
        }

        // Métodos auxiliares privados

        private async Task<SensoresModel?> GetSensorByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Sensores/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<SensoresModel>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private async Task<HttpResponseMessage> PostSensorAsync(SensoresModel sensor)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(sensor), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("api/Sensores", jsonContent);
        }

        private async Task<HttpResponseMessage> PutSensorAsync(int id, SensoresModel sensor)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(sensor), Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"api/Sensores/{id}", jsonContent);
        }
    }
}
