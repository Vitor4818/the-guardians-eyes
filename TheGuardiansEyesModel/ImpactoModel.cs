namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;

    public class ImpactoModel
    {
        public required int Id { get; set; }

    //Chave estrangeira para ImpactoClassificacao
        public required int ImpactoClassificacaoId { get; set; }

        //Propriedade de navegação para ImpactoClassificacao
        public ImpactoClassificacaoModel? ImpactoClassificacao { get; set; }


        //Desastres que possuem essa classificação
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }
    }

