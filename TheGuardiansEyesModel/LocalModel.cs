namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;

public class LocalModel
{
    public int Id { get; set; }

    // Coordenadas geográficas obrigatórias
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }

    public string? Cep { get; set; }
    public string? Endereco { get; set; }
    public string? Municipio { get; set; }
    public string? Numero { get; set; }

    [JsonIgnore]
    public ICollection<PessoaLocalizadaModel>? PessoasLocalizadas { get; set; } = new List<PessoaLocalizadaModel>();

    // Relação reversa
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }
        [JsonIgnore]
        public ICollection<ImagensCapturadasModel> ImagensCapturadas { get; set; } = new List<ImagensCapturadasModel>();

}
