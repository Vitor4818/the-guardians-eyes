using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheGuardiansEyesModel;
using TheGuardiansEyesData;
using Microsoft.EntityFrameworkCore;

public class ImagensCapturadasService
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly string _apiPythonUrl = "http://127.0.0.1:5000/predict";

    public ImagensCapturadasService(AppDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    private class ClassificacaoResponse
    {
        public string Classe { get; set; } = "";
        public double Confianca { get; set; }
    }

    public async Task<ImagensCapturadasModel> CadastrarImagemAsync(ImagensCapturadasModel imagem)
    {
        var resposta = await _httpClient.PostAsJsonAsync(_apiPythonUrl, new { urlImagem = imagem.Hospedagem });

        if (resposta.IsSuccessStatusCode)
        {
            var classificacao = await resposta.Content.ReadFromJsonAsync<ClassificacaoResponse>();

            if (classificacao != null)
            {
                imagem.IdImpactoClassificacao = classificacao.Classe.ToLower() switch
                {
                    "leve" => 1,
                    "moderado" => 2,
                    "pesado" => 3,
                    _ => 1
                };
            }
            else
            {
                imagem.IdImpactoClassificacao = 1;
            }
        }
        else
        {
            imagem.IdImpactoClassificacao = 1;
        }

        _context.ImagensCapturadas.Add(imagem);
        await _context.SaveChangesAsync();
        return imagem;
    }

    // Os outros métodos podem continuar síncronos se quiser:
    public List<ImagensCapturadasModel> ListarImagens()
    {
        return _context.ImagensCapturadas
            .Include(i => i.Local)
            .Include(i => i.ImpactoClassificacao)
            .Include(i => i.Drone)
            .ToList();
    }

    public ImagensCapturadasModel? ObterPorId(int id)
    {
        return _context.ImagensCapturadas
            .Include(i => i.Local)
            .Include(i => i.ImpactoClassificacao)
            .Include(i => i.Drone)
            .FirstOrDefault(i => i.Id == id);
    }

    public bool AtualizarImagem(ImagensCapturadasModel imagem)
    {
        var existente = _context.ImagensCapturadas.Find(imagem.Id);
        if (existente == null) return false;

        existente.Hospedagem = imagem.Hospedagem;
        existente.Tamanho = imagem.Tamanho;
        existente.IdLocal = imagem.IdLocal;
        existente.IdImpactoClassificacao = imagem.IdImpactoClassificacao;
        existente.IdDrone = imagem.IdDrone;

        _context.ImagensCapturadas.Update(existente);
        _context.SaveChanges();
        return true;
    }

    public bool RemoverImagem(int id)
    {
        var imagem = _context.ImagensCapturadas.Find(id);
        if (imagem == null) return false;

        _context.ImagensCapturadas.Remove(imagem);
        _context.SaveChanges();
        return true;
    }
}
