using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheGuardiansEyesModel;
using System.Text.Json; 

namespace TheGuardiansEyesMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("Api");
        }

public async Task<IActionResult> Index()
{
    var response = await _httpClient.GetAsync("api/usuario");

    if (!response.IsSuccessStatusCode)
        return View(new List<UsuarioModel>());

    var content = await response.Content.ReadAsStringAsync();

    if (string.IsNullOrWhiteSpace(content))
        return View(new List<UsuarioModel>());

    var usuarios = JsonSerializer.Deserialize<List<UsuarioModel>>(content,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return View(usuarios ?? new List<UsuarioModel>()); 
}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            var response = await _httpClient.PostAsJsonAsync("api/usuario", usuario);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao criar usuário: {errorContent}");
            return View(usuario);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _httpClient.GetFromJsonAsync<UsuarioModel>($"api/usuario/{id}");
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            var response = await _httpClient.PutAsJsonAsync($"api/usuario/{usuario.Id}", usuario);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao atualizar usuário.");
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/usuario/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var errorContent = await response.Content.ReadAsStringAsync();
            TempData["Erro"] = errorContent;

            return RedirectToAction(nameof(Index));
        }
    }
}
