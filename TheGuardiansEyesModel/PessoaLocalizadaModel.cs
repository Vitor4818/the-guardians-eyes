namespace TheGuardiansEyesModel;

public class PessoaLocalizadaModel
{
    public int Id { get; set; }
    public required int IdLocal { get; set; }
    public LocalModel? Local { get; set; }
    public required DateTime DataHora { get; set; }
    public required int IdImpactoClassificacao { get; set; }
    public ImpactoClassificacaoModel? ImpactoClassificacao { get; set; }

}