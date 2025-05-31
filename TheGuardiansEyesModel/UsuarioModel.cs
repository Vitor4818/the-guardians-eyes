namespace TheGuardiansEyesModel;
using System.Text.Json.Serialization;


public class UsuarioModel
{

    public required int Id { get; set; }
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required string Cpf { get; set; }
    public required string Cargo { get; set; }
    public required string Funcao { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }

    // Desastres criados/relacionados ao usuário
    [JsonIgnore]
    public ICollection<DesastreModel>? Desastres { get; set; }

}
