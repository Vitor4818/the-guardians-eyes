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
    try
    {
        //Envia a URL da imagem capturada para a API Python que classifica o impacto
        var resposta = await _httpClient.PostAsJsonAsync(_apiPythonUrl, new { urlImagem = imagem.Hospedagem });

        if (resposta.IsSuccessStatusCode)
        {
            //Lê a resposta da classificação (classe e confiança)
            var classificacao = await resposta.Content.ReadFromJsonAsync<ClassificacaoResponse>();

            if (classificacao != null)
            {
                //Converte a string da classe para o respectivo Id de ImpactoClassificacao
                imagem.IdImpactoClassificacao = classificacao.Classe.ToLower() switch
                {
                    "leve" => 1,
                    "moderado" => 2,
                    "pesado" => 3,
                    _ => 1 // Default para "leve" se vier algo desconhecido
                };
            }
            else
            {
                imagem.IdImpactoClassificacao = 1; //Default em caso de falha na leitura da resposta
            }
        }
        else
        {
            imagem.IdImpactoClassificacao = 1; //Default em caso de erro na chamada
        }

        //Adiciona a imagem no banco de dados
        _context.ImagensCapturadas.Add(imagem);

        //Cria o registro de PessoaLocalizada (associando pessoa detectada ao local da imagem)
        var pessoaLocalizada = new PessoaLocalizadaModel
        {
            IdLocal = imagem.IdLocal, //Pega o local da imagem
            DataHora = DateTime.Now, //Hora atual da detecção
            IdImpactoClassificacao = imagem.IdImpactoClassificacao ?? 1 // Mesmo impacto da imagem
        };

        //Adiciona a pessoa localizada no banco de dados
        _context.PessoasLocalizadas.Add(pessoaLocalizada);

        //Salva as duas entidades no banco: imagem e pessoa localizada
        await _context.SaveChangesAsync();

        //Retorna a imagem cadastrada
        return imagem;
    }
    catch (DbUpdateException ex)
    {
        throw new InvalidOperationException("Erro ao cadastrar imagem. Verifique os dados fornecidos.", ex);
    }
    catch (HttpRequestException ex)
    {
        throw new InvalidOperationException("Erro ao comunicar com a API de classificação de imagem.", ex);
    }
}

    public List<ImagensCapturadasModel> ListarImagens()
    {
        return _context.ImagensCapturadas
            .Include(i => i.Local)
            .Include(i => i.ImpactoClassificacao)
            .Include(i => i.Drone)
            .Include(i => i.Desastre)
            .ToList();
    }

    public ImagensCapturadasModel ObterPorId(int id)
    {
        var imagem = _context.ImagensCapturadas
            .Include(i => i.Local)
            .Include(i => i.ImpactoClassificacao)
            .Include(i => i.Drone)
            .FirstOrDefault(i => i.Id == id);

        if (imagem == null)
            throw new KeyNotFoundException("Imagem não encontrada.");

        return imagem;
    }

    public ImagensCapturadasModel AtualizarImagem(ImagensCapturadasModel imagem)
    {
        var existente = _context.ImagensCapturadas.Find(imagem.Id);
        if (existente == null)
        {
            throw new KeyNotFoundException("Imagem para atualização não encontrada.");
        }
        existente.Hospedagem = imagem.Hospedagem;
        existente.Tamanho = imagem.Tamanho;
        existente.IdLocal = imagem.IdLocal;
        existente.IdImpactoClassificacao = imagem.IdImpactoClassificacao;
        existente.IdDrone = imagem.IdDrone;

        try
        {
            _context.ImagensCapturadas.Update(existente);
            _context.SaveChanges();
            return existente;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Erro ao atualizar imagem. Verifique os dados fornecidos.", ex);
        }
    }

    public void RemoverImagem(int id)
    {
        var imagem = _context.ImagensCapturadas.Find(id);
        if (imagem == null)
            throw new KeyNotFoundException("Imagem para exclusão não encontrada.");

        try
        {
            _context.ImagensCapturadas.Remove(imagem);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Erro ao remover imagem. Verifique se há vínculos com outros dados.", ex);
        }
    }
}
