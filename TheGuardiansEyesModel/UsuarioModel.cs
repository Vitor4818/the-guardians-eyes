namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;


public class UsuarioModel
{

    public int? Id { get; set; }
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Cpf { get; set; }
    public string? Cargo { get; set; }
    public string? Funcao { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }

    // Desastres criados/relacionados ao usuário
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }

}
