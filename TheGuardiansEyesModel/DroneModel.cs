namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;


public class DroneModel
{

    public required int Id { get; set; }
    public required string Fabricante { get; set; }
    public required string Modelo { get; set; }
    public required string TempoVoo { get; set; }
    public required int DistanciaMaxima { get; set; }
    public required int VelocidadeMaxima { get; set; }
    public required string Camera { get; set; }
    public required int Peso { get; set; }

    [JsonIgnore]
    public ICollection<ImagensCapturadasModel>? ImagensCapturadas { get; set; }

}
