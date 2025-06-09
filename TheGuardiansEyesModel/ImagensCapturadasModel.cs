namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;



public class ImagensCapturadasModel
{
    public required int Id { get; set; }

    // Informação da imagem
    public required string Hospedagem { get; set; }  // pode ser URL ou caminho do arquivo
    public required string Tamanho { get; set; }    // ex: "1024x768"

    // FK para Local
    public required int IdLocal { get; set; }
    public LocalModel? Local { get; set; }

    // FK para ImpactoClassificacao
    public int? IdImpactoClassificacao { get; set; }
    public ImpactoClassificacaoModel? ImpactoClassificacao { get; set; }

    // FK para Drone
    public required int IdDrone { get; set; }
    public DroneModel? Drone { get; set; }
    public  int IdDesastre { get; set; }
    [JsonIgnore]
    public DesastreModel? Desastre { get; set; }
}
