namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;

public class TerrenoGeograficoModel
{

    public required int Id { get; set; }
    public required string NomeTerreno { get; set; }

    // Relacionamento com Desastres
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }
}
