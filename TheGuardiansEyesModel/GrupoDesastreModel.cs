namespace TheGuardiansEyesModel;

public class GrupoDesastreModel
{

    public required int Id { get; set; }
    public required string NomeGrupo { get; set; }

    // Relacionamento de navegação
    public List<SubGrupoDesastreModel>? Subgrupos { get; set; }
    
    
    // Desastres relacionados a esse grupo
    public ICollection<DesastreModel>? Desastres { get; set; }

}
