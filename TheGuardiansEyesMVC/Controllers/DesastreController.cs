using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using TheGuardiansEyesModel;
using System.Collections.Generic;

namespace TheGuardiansEyesWeb.Controllers
{
    public class DesastreController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DesastreController> _logger;

        public DesastreController(IHttpClientFactory httpClientFactory, ILogger<DesastreController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
            _logger = logger;
        }

        private async Task<T?> DeserializarResposta<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return default;

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // GET: Desastre
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/desastre");
                var lista = await DeserializarResposta<List<DesastreModel>>(response) ?? new List<DesastreModel>();
                return View(lista);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar desastres.");
                TempData["Erro"] = "Erro ao conectar com a API.";
                return View(new List<DesastreModel>());
            }
        }

        // GET: Desastre/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/desastre/{id}");
                var desastre = await DeserializarResposta<DesastreModel>(response);

                if (desastre == null)
                    return NotFound();

                return View(desastre);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao obter detalhes do desastre.");
                TempData["Erro"] = "Erro ao conectar com a API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Desastre/Create
        public IActionResult Create() => View();

        // POST: Desastre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DesastreModel desastre)
        {
            if (!ModelState.IsValid)
                return View(desastre);

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(desastre), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/desastre", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Erro ao criar desastre: {erro}", errorContent);
                TempData["Erro"] = $"Erro ao criar desastre: {errorContent}";
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de rede ao criar desastre.");
                TempData["Erro"] = "Erro ao conectar com a API.";
            }

            return View(desastre);
        }

        // GET: Desastre/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/desastre/{id}");
                var desastre = await DeserializarResposta<DesastreModel>(response);

                if (desastre == null)
                    return NotFound();

                return View(desastre);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar desastre para edição.");
                TempData["Erro"] = "Erro ao conectar com a API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Desastre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DesastreModel desastre)
        {
            if (id != desastre.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(desastre);

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(desastre), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/desastre/{id}", content);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Erro ao editar desastre: {erro}", errorContent);
                TempData["Erro"] = $"Erro ao editar desastre: {errorContent}";
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de rede ao editar desastre.");
                TempData["Erro"] = "Erro ao conectar com a API.";
            }

            return View(desastre);
        }

        // GET: Desastre/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/desastre/{id}");
                var desastre = await DeserializarResposta<DesastreModel>(response);

                if (desastre == null)
                    return NotFound();

                return View(desastre);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar desastre para exclusão.");
                TempData["Erro"] = "Erro ao conectar com a API.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Desastre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/desastre/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                TempData["Erro"] = "Erro ao deletar o desastre.";
                _logger.LogWarning("Erro ao deletar desastre com id {id}", id);
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro de rede ao deletar desastre.");
                TempData["Erro"] = "Erro ao conectar com a API.";
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}
