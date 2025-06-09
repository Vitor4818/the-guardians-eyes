// UsuarioRequest.cs
namespace TheGuardiansEyesMVC.Models.Requests
{
    public class UsuarioRequest
    {
        public int Id { get; set; } // pode ser 0 no create
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Cpf { get; set; }
        public string? Cargo { get; set; }
        public string? Funcao { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
