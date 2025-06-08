using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheGuardiansEyesModel;

namespace TheGuardiansEyesMVC.Controllers
{
    public class GrupoDesastreController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public GrupoDesastreController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("Api");
        }

        // GET: GrupoDesastre
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/GrupoDesastre");

            if (!response.IsSuccessStatusCode)
                return View(new List<GrupoDesastreModel>());

            var grupos = await DeserializeResponse<List<GrupoDesastreModel>>(response);
            return View(grupos ?? new List<GrupoDesastreModel>());
        }

        // GET: GrupoDesastre/Create
        public IActionResult Create() => View();

        // POST: GrupoDesastre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrupoDesastreModel grupo)
        {
            if (!ModelState.IsValid)
                return View(grupo);

            var response = await _httpClient.PostAsJsonAsync("api/GrupoDesastre", grupo);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, error);
            return View(grupo);
        }

        // GET: GrupoDesastre/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var grupo = await GetGrupoById(id);
            if (grupo == null)
                return NotFound();

            return View(grupo);
        }

        // POST: GrupoDesastre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GrupoDesastreModel grupo)
        {
            if (id != grupo.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(grupo);

            var response = await _httpClient.PutAsJsonAsync($"api/GrupoDesastre/{id}", grupo);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, error);
            return View(grupo);
        }

        // GET: GrupoDesastre/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var grupo = await GetGrupoById(id);
            if (grupo == null)
                return NotFound();

            return View(grupo);
        }

        // POST: GrupoDesastre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GrupoDesastre/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();

            var grupo = await GetGrupoById(id) ?? new GrupoDesastreModel { Id = id, NomeGrupo = string.Empty };

            ViewBag.Erro = error;
            return View("Delete", grupo);
        }

        // Método privado para desserializar o conteúdo JSON da resposta
        private async Task<T?> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch
            {
                // Você pode logar o erro aqui se quiser
                return default;
            }
        }

        // Método privado para buscar GrupoDesastre pelo ID
        private async Task<GrupoDesastreModel?> GetGrupoById(int id)
        {
            var response = await _httpClient.GetAsync($"api/GrupoDesastre/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await DeserializeResponse<GrupoDesastreModel>(response);
        }
    }
}
