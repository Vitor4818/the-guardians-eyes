using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheGuardiansEyesMVC.Models.Responses;

namespace TheGuardiansEyesMVC.Controllers
{
    public class TerrenoGeograficoController : Controller
    {
        private readonly HttpClient _httpClient;

        public TerrenoGeograficoController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("Api");
        }

        public async Task<IActionResult> Index()
        {
            var terrenos = await _httpClient.GetFromJsonAsync<List<TerrenoGeograficoResponse>>("api/terrenogeografico");
            return View(terrenos);
        }
    }
}
