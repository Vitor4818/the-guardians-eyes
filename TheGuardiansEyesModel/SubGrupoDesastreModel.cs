namespace TheGuardiansEyesModel;


public class SubGrupoDesastreModel
{

    public int Id { get; set; }
    public required string Subgrupo { get; set; }
    public required string Tipo { get; set; }
    public required string SubTipo { get; set; }
    //Chave estrangeira
    public required int GrupoDesastreId { get; set; }
    public GrupoDesastreModel? GrupoDesastre { get; set; }
    
}
