using System.ComponentModel.DataAnnotations;

namespace TheGuardiansEyesMVC.Models.Requests
{
    
public class SubGrupoDesastreRequest
{
    public int Id { get; set; }
    public required string Subgrupo { get; set; }
    public required string Tipo { get; set; }
    public required string SubTipo { get; set; }
    public required int GrupoDesastreId { get; set; }
}

    
}
