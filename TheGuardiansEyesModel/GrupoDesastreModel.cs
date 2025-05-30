namespace TheGuardiansEyesModel;

public class GrupoDesastreModel
{

    public required int Id { get; set; }
    public required string NomeGrupo { get; set; }
    
    //Vai receber fk do model Subgrupo
    public required int SubGrupo { get; set; }

}
