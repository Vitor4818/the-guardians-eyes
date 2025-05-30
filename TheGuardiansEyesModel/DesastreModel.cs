namespace TheGuardiansEyesModel;

public class DesastreModel
{

    public required int Id { get; set; }

    //FK de LOCALMODEL
    public required int IdLocal { get; set; }

    //FK DE IMPACTOCLASSIFICACAO
    public required int IdImpactoClassificacao { get; set; }

    //FK DE GRUPO DESASTRE
    public required int IdGrupoDesastre { get; set; }

    //FK DE USUARIO
    public required int IdUsuario { get; set; }
    public required int Cobrade { get; set; }
    public required string DataOcorrencia { get; set; }
}
