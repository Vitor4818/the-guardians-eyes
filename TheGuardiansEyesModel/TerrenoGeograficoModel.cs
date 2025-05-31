namespace TheGuardiansEyesModel;

public class TerrenoGeograficoModel
{

    public required int Id { get; set; }
    public required string NomeTerreno { get; set; }
    
    // Relacionamento com Desastres
    public ICollection<DesastreModel>? Desastres { get; set; }
}
