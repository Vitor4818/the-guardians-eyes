namespace TheGuardiansEyesModel;

public class SubGrupoDesastreModel
{

    public required int Id { get; set; }
    public required string Subgrupo { get; set; }
    public required string Tipo { get; set; }
    public required string SubTipo { get; set; }

    // Chave estrangeira
    public required int GrupoDesastreId { get; set; }
    
     // Propriedade de navegação
    public GrupoDesastreModel? GrupoDesastre { get; set; }
    
}
