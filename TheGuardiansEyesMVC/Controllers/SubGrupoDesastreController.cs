using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using TheGuardiansEyesMVC.Models.Requests;
using TheGuardiansEyesMVC.Models.Responses;
using System.Text.Json;

namespace TheGuardiansEyesMVC.Controllers
{
    public class SubGrupoDesastreController : Controller
    {
        private readonly HttpClient _httpClient;

        public SubGrupoDesastreController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
        }

        // GET: SubGrupoDesastre
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/SubGrupoDesastre");

                if (!response.IsSuccessStatusCode)
                    return View(new List<SubGrupoDesastreResponse>());

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(content))
                    return View(new List<SubGrupoDesastreResponse>());

                var subgrupos = JsonSerializer.Deserialize<List<SubGrupoDesastreResponse>>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(subgrupos);
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Erro na comunicação com a API.");
                return View(new List<SubGrupoDesastreResponse>());
            }
        }

        // GET: SubGrupoDesastre/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subgrupo = await GetByIdAsync<SubGrupoDesastreResponse>(id);
            if (subgrupo == null)
                return NotFound();

            return View(subgrupo);
        }

        // GET: SubGrupoDesastre/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubGrupoDesastreRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var success = await ExecuteRequestAsync(() => _httpClient.PostAsJsonAsync("api/SubGrupoDesastre", request));
            if (success)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        // GET: SubGrupoDesastre/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subgrupo = await GetByIdAsync<SubGrupoDesastreRequest>(id);
            if (subgrupo == null)
                return NotFound();

            return View(subgrupo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubGrupoDesastreRequest request)
        {
            if (id != request.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(request);

            var success = await ExecuteRequestAsync(() => _httpClient.PutAsJsonAsync($"api/SubGrupoDesastre/{id}", request));
            if (success)
                return RedirectToAction(nameof(Index));

            return View(request);
        }

        // GET: SubGrupoDesastre/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subgrupo = await GetByIdAsync<SubGrupoDesastreResponse>(id);
            if (subgrupo == null)
                return NotFound();

            return View(subgrupo);
        }

        // POST: SubGrupoDesastre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await ExecuteRequestAsync(() => _httpClient.DeleteAsync($"api/SubGrupoDesastre/{id}"));
            if (success)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }


        private async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/SubGrupoDesastre/{id}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var obj = await response.Content.ReadFromJsonAsync<T>();
                return obj;
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Erro na comunicação com a API.");
                return null;
            }
        }

        private async Task AddErrorsFromResponseAsync(HttpResponseMessage response)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                var json = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent);
                if (json != null && json.ContainsKey("error"))
                    ModelState.AddModelError(string.Empty, json["error"]);
                else
                    ModelState.AddModelError(string.Empty, errorContent);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, errorContent);
            }
        }

        private async Task<bool> ExecuteRequestAsync(Func<Task<HttpResponseMessage>> httpAction)
        {
            try
            {
                var response = await httpAction();
                if (response.IsSuccessStatusCode)
                    return true;

                await AddErrorsFromResponseAsync(response);
                return false;
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Erro na comunicação com a API.");
                return false;
            }
        }
    }
}
