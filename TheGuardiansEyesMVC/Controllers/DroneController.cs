using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheGuardiansEyesModel;
using System.Collections.Generic;

namespace TheGuardiansEyesWeb.Controllers
{
    public class DroneController : Controller
    {
        private readonly HttpClient _httpClient;

        public DroneController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        // Método genérico para obter objeto por GET e já tratar erro e nulo
        private async Task<T?> GetObjectAsync<T>(string url) where T : class
        {
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;

            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Método para extrair mensagem de erro da resposta HTTP
        private async Task<string> GetErrorMessageAsync(HttpResponseMessage response)
        {
            var error = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(error) ? "Erro desconhecido." : error;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/drone");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Erro"] = $"Erro ao obter drones: {response.StatusCode}";
                return View(new List<DroneModel>());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                ViewBag.Mensagem = "Nenhum drone cadastrado.";
                return View(new List<DroneModel>());
            }

            var drones = await response.Content.ReadFromJsonAsync<List<DroneModel>>();
            return View(drones);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DroneModel drone)
        {
            if (!ModelState.IsValid) return View(drone);

            var response = await _httpClient.PostAsJsonAsync("api/drone", drone);

            if (response.IsSuccessStatusCode)
            {
                TempData["Sucesso"] = "Drone criado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, await GetErrorMessageAsync(response));
            return View(drone);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var drone = await GetObjectAsync<DroneModel>($"api/drone/{id}");
            if (drone == null)
            {
                TempData["Erro"] = "Drone não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(drone);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DroneModel drone)
        {
            if (!ModelState.IsValid) return View(drone);

            var response = await _httpClient.PutAsJsonAsync($"api/drone/{drone.Id}", drone);

            if (response.IsSuccessStatusCode)
            {
                TempData["Sucesso"] = "Drone atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, await GetErrorMessageAsync(response));
            return View(drone);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var drone = await GetObjectAsync<DroneModel>($"api/drone/{id}");
            if (drone == null)
            {
                TempData["Erro"] = "Drone não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(drone);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/drone/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Sucesso"] = "Drone deletado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Erro"] = await GetErrorMessageAsync(response);
            return RedirectToAction(nameof(Delete), new { id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var drone = await GetObjectAsync<DroneModel>($"api/drone/{id}");
            if (drone == null)
            {
                TempData["Erro"] = "Drone não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            return View(drone);
        }
    }
}
