namespace TheGuardiansEyesModel;

public class DesastreModel
{
    public required int Id { get; set; }

    // FK e navegação para Local
    public required int IdLocal { get; set; }
    public LocalModel? Local { get; set; }

    // FK e navegação para ImpactoClassificacao
    public required int IdImpactoClassificacao { get; set; }
    public ImpactoClassificacaoModel? ImpactoClassificacao { get; set; }

    // FK e navegação para GrupoDesastre
    public required int IdGrupoDesastre { get; set; }
    public GrupoDesastreModel? GrupoDesastre { get; set; }

    // FK e navegação para Usuario
    public required int IdUsuario { get; set; }
    public UsuarioModel? Usuario { get; set; }

    public required int Cobrade { get; set; }
    public required string DataOcorrencia { get; set; }
}

