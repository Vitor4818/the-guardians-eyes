namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;

public class GrupoDesastreModel
{

    public required int Id { get; set; }
    public required string NomeGrupo { get; set; }

    // Relacionamento de navegação
    [JsonIgnore]
    public List<SubGrupoDesastreModel>? Subgrupos { get; set; }


    // Desastres relacionados a esse grupo
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }

}
